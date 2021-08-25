using AutoMapper;
using CoreServices.DTO;
using CoreServices.Models;

namespace CoreServices.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}
