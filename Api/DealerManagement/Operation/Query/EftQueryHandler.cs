using AutoMapper;
using MediatR;
using Data.Domain;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Base.Response;

namespace Operation.Query
{

    public class EftQueryHandler :
        IRequestHandler<GetAllEftQuery, ApiResponse<List<EftResponse>>>,
        IRequestHandler<GetEftByIdQuery, ApiResponse<EftResponse>>
    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public EftQueryHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        public async Task<ApiResponse<List<EftResponse>>> Handle(GetAllEftQuery request,
            CancellationToken cancellationToken)
        {
            List<Eft> list = await dbContext.Set<Eft>()
                .Include(x => x.Account)
                .ToListAsync(cancellationToken);

            List<EftResponse> mapped = mapper.Map<List<EftResponse>>(list);
            return new ApiResponse<List<EftResponse>>(mapped);
        }

        public async Task<ApiResponse<EftResponse>> Handle(GetEftByIdQuery request,
            CancellationToken cancellationToken)
        {
            Eft? entity = await dbContext.Set<Eft>()
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<EftResponse>("Record not found!");
            }

            EftResponse mapped = mapper.Map<EftResponse>(entity);
            return new ApiResponse<EftResponse>(mapped);
        }
    }
}
