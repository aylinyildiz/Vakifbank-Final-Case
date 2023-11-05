using Api.Middleware;
using Base.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operation.Cqrs;
using Schema;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("dealer/api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IMediator mediator;

        public TokenController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
        {
            var operation = new CreateTokenCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }

        [TypeFilter(typeof(LogResourceFilter))]
        [TypeFilter(typeof(LogActionFilter))]
        [TypeFilter(typeof(LogAuthorizationFilter))]
        [TypeFilter(typeof(LogResourceFilter))]
        [TypeFilter(typeof(LogExceptionFilter))]
        [HttpGet("Test")]
        public ApiResponse Get()
        {
            return new ApiResponse();
        }
    }
}
