using AutoMapper;
using BusinessObject;
using eStore.Models.Dto;

namespace eStore
{
    public class MappingConfig : Profile
    {
        public MappingConfig() { 
            CreateMap<Product, ProductDTO>();
        }
    }
}
