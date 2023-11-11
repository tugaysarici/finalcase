using AutoMapper;
using Oms.Data.Domain;
using Oms.Schema;

namespace Oms.Operation.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();

        CreateMap<ProductRequest, Product>();
        CreateMap<Product, ProductResponse>();

        CreateMap<ChatRequest, Chat>();
        CreateMap<Chat, ChatResponse>();

        CreateMap<OrderRequest, Order>();
        CreateMap<Order, OrderResponse>();

        CreateMap<OrderItemRequest, OrderItem>();
        CreateMap<OrderItem, OrderItemResponse>();
    }
}