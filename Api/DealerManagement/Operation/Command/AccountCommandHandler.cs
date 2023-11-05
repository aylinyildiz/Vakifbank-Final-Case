using AutoMapper;
using Base.Response;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema;

namespace Operation.Command
{

    public class AccountCommandHandler :
        IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
        IRequestHandler<UpdateAccountCommand, ApiResponse>,
        IRequestHandler<DeleteAccountCommand, ApiResponse>

    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public AccountCommandHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Account mapped = mapper.Map<Account>(request.Model);
            var entity = await dbContext.Set<Account>().AddAsync(mapped, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<AccountResponse>(entity.Entity);
            return new ApiResponse<AccountResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            entity.Name = request.Model.Name;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Account>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }

            entity.IsActive = false;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
