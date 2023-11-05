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
    public record CreateCardCommand(CardRequest Model) : IRequest<ApiResponse<CardResponse>>;
    public record UpdateCardCommand(CardRequest Model, int Id) : IRequest<ApiResponse>;
    public record DeleteCardCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllCardQuery() : IRequest<ApiResponse<List<CardResponse>>>;
    public record GetCardByIdQuery(int Id) : IRequest<ApiResponse<CardResponse>>;
    public record GetCardByUserIdQuery(int UserId) : IRequest<ApiResponse<List<CardResponse>>>;
    public record GetCardByAccountIdQuery(int AccountId) : IRequest<ApiResponse<CardResponse>>;
}
