using AutoMapper;

namespace HomeWallet.Models.Maps
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Domain.Enteties.Category, CategoryDTO>()
                .ForMember(x => x.Name, y => y.MapFrom(c => c.Name))
                .ForMember(x => x.Description, y => y.MapFrom(c => c.Description))
                .ForMember(x => x.Operations, y => y.Ignore())
                .ForMember(x => x.CategoryType, y => y.MapFrom(c => c.CategoryType));

            CreateMap<CategoryDTO, Domain.Enteties.Category>()
                .ForMember(x => x.Name, y => y.MapFrom(c => c.Name))
                .ForMember(x => x.Description, y => y.MapFrom(c => c.Description))
                .ForMember(x => x.Operations, y => y.Ignore())
                .ForMember(x => x.CategoryType, y => y.MapFrom(c => c.CategoryType));
        }
    }
}