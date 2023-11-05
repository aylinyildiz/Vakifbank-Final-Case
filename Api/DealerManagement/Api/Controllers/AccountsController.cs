using Base.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operation.Cqrs;
using Schema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("dealer/api/v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IMediator mediator;

        public AccountsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<AccountResponse>>> GetAll()
        {
            var operation = new GetAllAccountQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<AccountResponse>> Get(int id)
        {
            var operation = new GetAccountByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByUserId/{Userid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<AccountResponse>>> GetByUserId(int Userid)
        {
            var operation = new GetAccountByUserIdQuery(Userid);
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest request)
        {
            var operation = new CreateAccountCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] AccountRequest request)
        {
            var operation = new UpdateAccountCommand(request, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteAccountCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
