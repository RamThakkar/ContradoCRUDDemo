using API.Dtos;
using API.Models;
using AutoMapper;


namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProdCatName, o => o.MapFrom(s => s.ProdCat.CategoryName));
                
        }
    }
}