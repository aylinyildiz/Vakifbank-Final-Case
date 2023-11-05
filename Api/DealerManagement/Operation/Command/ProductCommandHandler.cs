using AutoMapper;
using Base.Response;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema;

namespace Operation.Command
{
    public class ProductCommandHandler :
         IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>,
        IRequestHandler<UpdateProductCommand, ApiResponse>,
        IRequestHandler<DeleteProductCommand, ApiResponse>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public ProductCommandHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product mapped = mapper.Map<Product>(request.Model);
            var entity = await dbContext.Set<Product>().AddAsync(mapped, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<ProductResponse>(entity.Entity);
            return new ApiResponse<ProductResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            entity.Name = request.Model.Name;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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
