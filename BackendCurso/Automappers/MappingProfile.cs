using AutoMapper;
using BackendCurso.DTOs;
using BackendCurso.Models;

namespace BackendCurso.Automappers
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            CreateMap<BeerInsertDto, Beer>();
            CreateMap<Beer, BeerDto>()
                .ForMember(dto => dto.Id, m => m.MapFrom(beer => beer.BeerId));

            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}
