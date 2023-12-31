﻿using Base.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operation.Cqrs;
using Schema;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("dealer/api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<List<OrderResponse>>> GetAll()
        {
            var operation = new GetAllOrderQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetOrderReportsQuery")]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<List<LowStock>>> GetOrderReportsQuery()
        {
            var operation = new GetOrderReportsQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<OrderResponse>> Get(int id)
        {
            var operation = new GetOrderByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByUserId/{Userid}")]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<List<OrderResponse>>> GetByUserId(int Userid)
        {
            var operation = new GetOrderByUserIdQuery(Userid);
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpPost]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<OrderResponse>> Post([FromBody] OrderRequest request)
        {
            var operation = new CreateOrderCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("UpdateOrderStatus/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> UpdateOrderStatus(int id, [FromBody] OrderRequest request)
        {
            var operation = new UpdateOrderStatusCommand(request, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("UpdateOrderByDealer/{id}")]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse> UpdateOrderByDealer(int id, [FromBody] OrderRequest request)
        {
            var operation = new UpdateOrderByDealerCommand(request, id);
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] OrderRequest request)
        {
            var operation = new UpdateOrderCommand(request, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteOrderCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
