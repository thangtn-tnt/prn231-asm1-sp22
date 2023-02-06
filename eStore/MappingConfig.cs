using AutoMapper;
using BusinessObject;
using eStore.Models.Dto;

namespace eStore
{
    public class MappingConfig : Profile
    {
        public MappingConfig() { 
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<ProductDTO, ProductUpdateDTO>().ReverseMap();
            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<ProductDTO, OrderDetailDTO>().ReverseMap();
        }
    }
}
