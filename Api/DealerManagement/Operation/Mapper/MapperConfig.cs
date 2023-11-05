using AutoMapper;
using Data.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();

            CreateMap<AccountRequest, Account>();
            CreateMap<Account, AccountResponse>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<CardRequest, Card>();
            CreateMap<Card, CardResponse>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber));

            CreateMap<AddressRequest, Address>();
            CreateMap<Address, AddressResponse>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<EftRequest, Eft>();
            CreateMap<Eft, EftResponse>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Account.User.FirstName + " " + src.Account.User.LastName));

            CreateMap<AccountTransactionRequest, AccountTransaction>();
            CreateMap<AccountTransaction, AccountTransactionResponse>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Account.User.FirstName + " " + src.Account.User.LastName));
        }
    }
}
