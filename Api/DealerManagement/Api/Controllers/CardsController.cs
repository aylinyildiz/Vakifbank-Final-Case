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
    public class CardsController : ControllerBase
    {
        private IMediator mediator;

        public CardsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<CardResponse>>> GetAll()
        {
            var operation = new GetAllCardQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<CardResponse>> Get(int id)
        {
            var operation = new GetCardByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByUserId/{Userid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<CardResponse>>> GetByUserId(int Userid)
        {
            var operation = new GetCardByUserIdQuery(Userid);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByAccountId/{accountid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<CardResponse>> GetByAccountId(int accountid)
        {
            var operation = new GetCardByAccountIdQuery(accountid);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<CardResponse>> Post([FromBody] CardRequest request)
        {
            var operation = new CreateCardCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] CardRequest request)
        {
            var operation = new UpdateCardCommand(request, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteCardCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
