using Base.Response;
using MediatR;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Cqrs
{
    public record CreateProductCommand(ProductRequest Model) : IRequest<ApiResponse<ProductResponse>>;
    public record UpdateProductCommand(ProductRequest Model, int Id) : IRequest<ApiResponse>;
    public record DeleteProductCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllProductQuery() : IRequest<ApiResponse<List<ProductResponse>>>;
    public record GetProductByIdQuery(int Id) : IRequest<ApiResponse<ProductResponse>>;
    public record GetProductByUserIdQuery(int UserId) : IRequest<ApiResponse<List<ProductResponse>>>;
}
