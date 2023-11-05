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
    public class UsersController : ControllerBase
    {
        private IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<UserResponse>>> GetAll()
        {
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<UserResponse>> Get(int id)
        {
            var operation = new GetUserByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest request)
        {
            var operation = new CreateUserCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] UserRequest request)
        {
            var operation = new UpdateUserCommand(request, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteUserCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}

