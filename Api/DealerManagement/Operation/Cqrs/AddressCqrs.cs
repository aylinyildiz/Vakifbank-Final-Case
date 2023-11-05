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
    public record CreateAddressCommand(AddressRequest Model) : IRequest<ApiResponse<AddressResponse>>;
    public record UpdateAddressCommand(AddressRequest Model, int Id) : IRequest<ApiResponse>;
    public record DeleteAddressCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllAddressQuery() : IRequest<ApiResponse<List<AddressResponse>>>;
    public record GetAddressByIdQuery(int Id) : IRequest<ApiResponse<AddressResponse>>;
    public record GetAddressByUserIdQuery(int UserId) : IRequest<ApiResponse<List<AddressResponse>>>;
}
