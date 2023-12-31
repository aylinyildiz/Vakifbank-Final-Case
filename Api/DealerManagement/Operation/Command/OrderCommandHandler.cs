﻿using AutoMapper;
using Base.Response;
using Data.Context;
using Data.Domain;
using Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema;

namespace Operation.Command
{
    public class OrderCommandHandler :
          IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>,
        IRequestHandler<UpdateOrderCommand, ApiResponse>,
        IRequestHandler<DeleteOrderCommand, ApiResponse>,
        IRequestHandler<UpdateOrderStatusCommand, ApiResponse>,
        IRequestHandler<UpdateOrderByDealerCommand, ApiResponse>
    {

        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public OrderCommandHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var productsControl = request.Model.ProductOrders;

            foreach (var item in productsControl)
            {
                Product? product = await dbContext.Set<Product>()
                .FirstOrDefaultAsync(x => x.Id == item.ProductId, cancellationToken);

                if (!(product.StockQuantity > item.ProductCount))
                {
                    return new ApiResponse<OrderResponse>("Stokta yeterli ürün bulunamadı.");
                }
            }

            Order mapped = mapper.Map<Order>(request.Model);

            mapped.UpdateDate = DateTime.Now;
            mapped.InsertDate = DateTime.Now;

            mapped.StatusId = (int)OrderStatus.Pending;

            var entity = await dbContext.Set<Order>().AddAsync(mapped, cancellationToken);


            foreach (var item in productsControl)
            {
                var stockUpdate = await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == item.ProductId, cancellationToken);

                stockUpdate.StockQuantity = stockUpdate.StockQuantity - item.ProductCount;
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<OrderResponse>(entity.Entity);

            return new ApiResponse<OrderResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            entity.Status.Id = request.Model.StatusId;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Order>().Include(x=>x.ProductOrders).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }

            entity.StatusId = request.Model.StatusId;

            if(entity.StatusId == (int)OrderStatus.Cancelled)
            {
                foreach (var item in entity.ProductOrders)
                {
                    var stockUpdate = await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == item.ProductId, cancellationToken);

                    stockUpdate.StockQuantity = stockUpdate.StockQuantity + item.ProductCount;
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
        public async Task<ApiResponse> Handle(UpdateOrderByDealerCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Order>().Include(x => x.ProductOrders).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }

            if (entity.StatusId == (int)OrderStatus.Pending)
            {
                entity.StatusId = (int)OrderStatus.Cancelled;

                entity.IsActive = false;

                foreach (var item in entity.ProductOrders)
                {
                    var stockUpdate = await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == item.ProductId, cancellationToken);

                    stockUpdate.StockQuantity = stockUpdate.StockQuantity + item.ProductCount;
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }

            entity.IsActive = false;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

       
    }
}
