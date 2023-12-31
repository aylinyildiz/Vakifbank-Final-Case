﻿using Base.Response;
using MediatR;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Cqrs
{

    public record CreateUserCommand(UserRequest Model) : IRequest<ApiResponse<UserResponse>>;
    public record UpdateUserCommand(UserRequest Model, int Id) : IRequest<ApiResponse>;
    public record DeleteUserCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
    public record GetUserByIdQuery(int Id) : IRequest<ApiResponse<UserResponse>>;
}
