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
    public record CreateEftCommand(EftRequest Model) : IRequest<ApiResponse<EftResponse>>;
    public record GetAllEftQuery() : IRequest<ApiResponse<List<EftResponse>>>;
    public record GetEftByIdQuery(int Id) : IRequest<ApiResponse<EftResponse>>;
}
