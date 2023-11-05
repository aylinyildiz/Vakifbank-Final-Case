using AutoMapper;
using Base.Response;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema;

namespace Operation.Query
{
    public class AccountQueryHandler :
        IRequestHandler<GetAllAccountQuery, ApiResponse<List<AccountResponse>>>,
        IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>,
        IRequestHandler<GetAccountByUserIdQuery, ApiResponse<List<AccountResponse>>>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public AccountQueryHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountQuery request,
            CancellationToken cancellationToken)
        {
            List<Account> list = await dbContext.Set<Account>().Include(x => x.User).ToListAsync(cancellationToken);

            List<AccountResponse> mapped = mapper.Map<List<AccountResponse>>(list);
            return new ApiResponse<List<AccountResponse>>(mapped);
        }

        public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request,
            CancellationToken cancellationToken)
        {
            Account? entity = await dbContext.Set<Account>().Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                return new ApiResponse<AccountResponse>("Record not found");
            }

            AccountResponse mapped = mapper.Map<AccountResponse>(entity);
            return new ApiResponse<AccountResponse>(mapped);
        }

        public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAccountByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            List<Account> list = await dbContext.Set<Account>().Include(x => x.User).Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken);

            List<AccountResponse> mapped = mapper.Map<List<AccountResponse>>(list);
            return new ApiResponse<List<AccountResponse>>(mapped);
        }
    }
}
