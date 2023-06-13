using AutoMapper;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Util;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Customer, CustomerDto>();
        CreateMap<FeaturedProduct, FeaturedProductDto>();
        CreateMap<Newsletter, NewsletterDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<User, UserDto>();
        CreateMap<WebsiteContent, WebsiteContentDto>();
    }
}