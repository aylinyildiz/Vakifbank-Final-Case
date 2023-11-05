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

namespace Operation.Command
{

    public class AddressCommandHandler :
        IRequestHandler<CreateAddressCommand, ApiResponse<AddressResponse>>,
        IRequestHandler<UpdateAddressCommand, ApiResponse>,
        IRequestHandler<DeleteAddressCommand, ApiResponse>

    {
        private readonly DealerDbContext dbContext;
        private readonly IMapper mapper;

        public AddressCommandHandler(DealerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        public async Task<ApiResponse<AddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            Address mapped = mapper.Map<Address>(request.Model);

            var entity = await dbContext.Set<Address>().AddAsync(mapped, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<AddressResponse>(entity.Entity);
            return new ApiResponse<AddressResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Address>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return new ApiResponse("Record not found!");
            }
            entity.AddressLine1 = request.Model.AddressLine1;
            entity.AddressLine2 = request.Model.AddressLine2;
            entity.County = request.Model.County;
            entity.City = request.Model.City;
            entity.PostalCode = request.Model.PostalCode;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Address>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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
