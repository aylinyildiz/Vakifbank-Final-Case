using AutoMapper;
using Base.Response;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Query
{
    public class OrderQueryHandler :
         IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>,
    IRequestHandler<GetOrderByUserIdQuery, ApiResponse<List<OrderResponse>>>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public OrderQueryHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            List<Order> list = await dbContext.Set<Order>()
             .Include(x => x.User)
             .Include(x => x.Status)
             .ToListAsync(cancellationToken);

            List<OrderResponse> mapped = mapper.Map<List<OrderResponse>>(list);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }

        public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Order? entity = await dbContext.Set<Order>()
           .Include(x => x.User)
           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<OrderResponse>("Record not found!");
            }

            OrderResponse mapped = mapper.Map<OrderResponse>(entity);
            return new ApiResponse<OrderResponse>(mapped);
        }

        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Order> list = await dbContext.Set<Order>()
          .Include(x => x.User)
          .Where(x => x.UserId == request.UserId)
          .ToListAsync(cancellationToken);

            var mapped = mapper.Map<List<OrderResponse>>(list);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }
    }
}
