using Base.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operation.Cqrs;
using Schema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("dealer/api/v1/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private IMediator mediator;

        public AddressesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<AddressResponse>>> GetAll()
        {
            var operation = new GetAllAddressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<AddressResponse>> Get(int id)
        {
            var operation = new GetAddressByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByUserId/{Userid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<AddressResponse>>> GetByUserId(int Userid)
        {
            var operation = new GetAddressByUserIdQuery(Userid);
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<AddressResponse>> Post([FromBody] AddressRequest request)
        {
            var operation = new CreateAddressCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] AddressRequest request)
        {
            var operation = new UpdateAddressCommand(request, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteAddressCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
