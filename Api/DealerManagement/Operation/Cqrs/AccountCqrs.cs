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
    public record CreateAccountCommand(AccountRequest Model) : IRequest<ApiResponse<AccountResponse>>;
    public record UpdateAccountCommand(AccountRequest Model, int Id) : IRequest<ApiResponse>;
    public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
    public record GetAccountByIdQuery(int Id) : IRequest<ApiResponse<AccountResponse>>;
    public record GetAccountByUserIdQuery(int UserId) : IRequest<ApiResponse<List<AccountResponse>>>;
}
