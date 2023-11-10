using AutoMapper;
using Base.Response;
using Dapper;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema;

namespace Operation.Query
{
    public class ProductQueryHandler :
        IRequestHandler<GetAllProductQuery, ApiResponse<List<ProductResponse>>>,
        IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>,
        IRequestHandler<GetProductsByUserIdQuery, ApiResponse<List<ProductResponse>>>,
        IRequestHandler<GetStockStatusByProductIdQuery, ApiResponse<List<ProductResponse>>>,
        IRequestHandler<GetProductStockReportQuery, ApiResponse<List<LowStock>>>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IDapperContext dapperContext;

        public ProductQueryHandler(DealerDbContext dbContext, IMapper mapper, IDapperContext dapperContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.dapperContext= dapperContext;
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> list = await dbContext.Set<Product>().Include(x => x.ProductUsers).ThenInclude(x => x.User).ToListAsync(cancellationToken);

            List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }

        public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? entity = await dbContext.Set<Product>().Include(x => x.ProductUsers).ThenInclude(x => x.User)
               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                return new ApiResponse<ProductResponse>("Record not found");
            }

            ProductResponse mapped = mapper.Map<ProductResponse>(entity);
            return new ApiResponse<ProductResponse>(mapped);
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetProductsByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Product> list = await dbContext.Set<Product>().Include(x => x.ProductUsers).ThenInclude(x => x.User).Where(x => x.Id == request.UserId).ToListAsync(cancellationToken);

            List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetStockStatusByProductIdQuery request, CancellationToken cancellationToken)
        {
            List<Product> list = await dbContext.Set<Product>().ToListAsync(cancellationToken);

            List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }
        
        public async Task<ApiResponse<List<LowStock>>> Handle(GetProductStockReportQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<LowStock> stocks = new List<LowStock>();
            using (var con = dapperContext.GetOpenConnection())
            {
                stocks = await con.QueryAsync<LowStock>("SELECT * FROM dbo.GetProductStockReport()");
                return new ApiResponse<List<LowStock>>((List<LowStock>)stocks);
            }
        }
    }
}
