using AutoMapper;
using CoreServices.Models;

namespace CoreServices.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDTO, Product>();
        }
    }
}
