using AutoMapper;
using Base.Response;
using Dapper;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Operation.Query
{
    public class OrderQueryHandler :
         IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>,
    IRequestHandler<GetOrderByUserIdQuery, ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetOrderReportsQuery, ApiResponse<List<LowStock>>>,
    IRequestHandler<GetOrderReportByUserIdQuery, ApiResponse<OrderResponse>>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IDapperContext dapperContext;

        public OrderQueryHandler(DealerDbContext dbContext, IMapper mapper, IDapperContext dapperContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.dapperContext = dapperContext;
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

        //public async Task<ApiResponse> Handle(GetOrderReportsQuery request, CancellationToken cancellationToken)
        //{
        //    using (var con = dapperContext.GetOpenConnection())
        //    {
        //        var data = (ApiResponse)await con.QueryAsync<ApiResponse>("dbo.GetLowStockProducts", commandType: CommandType.TableDirect);
        //        return data;
        //    }
        //}

        public async Task<ApiResponse<List<LowStock>>> Handle(GetOrderReportsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<LowStock> stocks = new List<LowStock>();
            using (var con = dapperContext.GetOpenConnection())
            {
                stocks =await con.QueryAsync<LowStock>("SELECT * FROM dbo.GetLowStockProducts()");
                return new ApiResponse<List<LowStock>>((List<LowStock>)stocks); 
            }
        }

        public async Task<ApiResponse<OrderResponse>> Handle(GetOrderReportByUserIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<OrderResponse> orders = new List<OrderResponse>();
            using (var con = dapperContext.GetOpenConnection())
            {
                orders = await con.QueryAsync<OrderResponse>($"SELECT * FROM dbo.GetOrderReports({request.Id})");
                return new ApiResponse<OrderResponse>((OrderResponse)orders);
            }
        }
    }
}
