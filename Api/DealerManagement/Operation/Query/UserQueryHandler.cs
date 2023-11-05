using AutoMapper;
using Base.Response;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Query
{
    public class UserQueryHandler :
        IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
        IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public UserQueryHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request,
            CancellationToken cancellationToken)
        {
            List<User> list = await dbContext.Set<User>()
                .Include(x => x.Accounts)
                .Include(x => x.Addresses)
                .ToListAsync(cancellationToken);

            List<UserResponse> mapped = mapper.Map<List<UserResponse>>(list);
            return new ApiResponse<List<UserResponse>>(mapped);
        }

        public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            User? entity = await dbContext.Set<User>()
                .Include(x => x.Accounts)
                .Include(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<UserResponse>("Record not found!");
            }

            UserResponse mapped = mapper.Map<UserResponse>(entity);
            return new ApiResponse<UserResponse>(mapped);
        }
    }
}
