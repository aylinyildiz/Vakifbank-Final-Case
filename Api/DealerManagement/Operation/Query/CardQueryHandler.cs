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

    public class CardQueryHandler :
        IRequestHandler<GetAllCardQuery, ApiResponse<List<CardResponse>>>,
        IRequestHandler<GetCardByIdQuery, ApiResponse<CardResponse>>,
        IRequestHandler<GetCardByUserIdQuery, ApiResponse<List<CardResponse>>>,
        IRequestHandler<GetCardByAccountIdQuery, ApiResponse<CardResponse>>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public CardQueryHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CardResponse>>> Handle(GetAllCardQuery request,
            CancellationToken cancellationToken)
        {
            List<Card> list = await dbContext.Set<Card>()
                .Include(x => x.Account)
                .ToListAsync(cancellationToken);

            List<CardResponse> mapped = mapper.Map<List<CardResponse>>(list);
            return new ApiResponse<List<CardResponse>>(mapped);
        }

        public async Task<ApiResponse<CardResponse>> Handle(GetCardByIdQuery request,
            CancellationToken cancellationToken)
        {
            Card? entity = await dbContext.Set<Card>()
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<CardResponse>("Record not found!");
            }

            CardResponse mapped = mapper.Map<CardResponse>(entity);
            return new ApiResponse<CardResponse>(mapped);
        }

        public async Task<ApiResponse<List<CardResponse>>> Handle(GetCardByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            List<Card> list = await dbContext.Set<Card>()
                .Include(x => x.Account)
                .Where(x => x.Account.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var mapped = mapper.Map<List<CardResponse>>(list);
            return new ApiResponse<List<CardResponse>>(mapped);
        }

        public async Task<ApiResponse<CardResponse>> Handle(GetCardByAccountIdQuery request,
            CancellationToken cancellationToken)
        {
            Card? entity = await dbContext.Set<Card>()
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.AccountId == request.AccountId, cancellationToken);

            CardResponse mapped = mapper.Map<CardResponse>(entity);
            return new ApiResponse<CardResponse>(mapped);
        }
    }
}
