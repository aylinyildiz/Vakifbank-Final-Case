using Base.Response;
using MediatR;
using Schema;

namespace Operation.Cqrs
{
    public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;
}
