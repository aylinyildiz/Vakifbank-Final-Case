using AutoMapper;
using Base.Response;
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
        IRequestHandler<GetProductByUserIdQuery, ApiResponse<List<ProductResponse>>>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public ProductQueryHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> list = await dbContext.Set<Product>().Include(x => x.User).ToListAsync(cancellationToken);

            List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }

        public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? entity = await dbContext.Set<Product>().Include(x => x.User)
               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                return new ApiResponse<ProductResponse>("Record not found");
            }

            ProductResponse mapped = mapper.Map<ProductResponse>(entity);
            return new ApiResponse<ProductResponse>(mapped);
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetProductByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Product> list = await dbContext.Set<Product>().Include(x => x.User).Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken);

            List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }
    }
}
