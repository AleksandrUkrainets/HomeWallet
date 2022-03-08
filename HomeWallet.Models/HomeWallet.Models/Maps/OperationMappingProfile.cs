using AutoMapper;

namespace HomeWallet.Models.Maps
{
    public class OperationMappingProfile : Profile
    {
        public OperationMappingProfile()
        {
            CreateMap<Domain.Enteties.Operation, OperationDTO>()
                .ForMember(x => x.Date, y => y.MapFrom(c => c.Date))
                .ForMember(x => x.Amount, y => y.MapFrom(c => c.Amount))
                .ForMember(x => x.CategoryId, y => y.MapFrom(c => c.CategoryId))
                .ForMember(x => x.Category, y => y.MapFrom(c => c.Category));

            CreateMap<OperationDTO, Domain.Enteties.Operation>()
                .ForMember(x => x.Date, y => y.MapFrom(c => c.Date))
                .ForMember(x => x.Amount, y => y.MapFrom(c => c.Amount))
                .ForMember(x => x.CategoryId, y => y.MapFrom(c => c.CategoryId))
                .ForMember(x => x.Category, y => y.Ignore());
        }
    }
}