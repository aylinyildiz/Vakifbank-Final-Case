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
    public class ProductsController : ControllerBase
    {
        private IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<List<ProductResponse>>> GetAll()
        {
            var operation = new GetAllProductQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<ProductResponse>> Get(int id)
        {
            var operation = new GetProductByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByUserId/{Userid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<ProductResponse>>> GetByUserId(int Userid)
        {
            var operation = new GetProductByUserIdQuery(Userid);
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpPost]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<ProductResponse>> Post([FromBody] ProductRequest request)
        {
            var operation = new CreateProductCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] ProductRequest request)
        {
            var operation = new UpdateProductCommand(request, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteProductCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
