using Base.Response;
using MediatR;
using Schema;

namespace Operation.Cqrs
{
    //public record CreateOrderCommand(OrderRequest Model) : IRequest<ApiResponse<OrderResponse>>;
    public record CreateOrderCommand(OrderRequest Model) : IRequest<ApiResponse<OrderResponse>>;
    public record UpdateOrderCommand(OrderRequest Model, int Id) : IRequest<ApiResponse>;
    public record DeleteOrderCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
    public record GetOrderByIdQuery(int Id) : IRequest<ApiResponse<OrderResponse>>;
    public record GetOrderByUserIdQuery(int UserId) : IRequest<ApiResponse<List<OrderResponse>>>;
}
