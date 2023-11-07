using AutoMapper;
using Data.Domain;
using Schema;

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

            CreateMap<OrderRequest, Order>()
                .ForMember(dest => dest.ProductOrders, opt => opt.MapFrom(src => src.ProductOrders));

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ProductUsers.FirstOrDefault().Id));

            CreateMap<ProductOrderRequest, ProductOrder>();
            CreateMap<ProductOrder, ProductOrderResponse>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id));

            CreateMap<ProductUserRequest, ProductUser>();
            CreateMap<ProductUser, ProductUserResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));

            CreateMap<ProductOrderRequest, Order>();
            CreateMap<Order, ProductOrderResponse>();
        }
    }
}
