using AutoMapper;
using Solar.Application.DTOs.Tecnico;

namespace Solar.Application.DTOs.Mapping;
using Solar.Domain.Entities;

public class TecnicoDtoMapping : Profile
{
    public TecnicoDtoMapping()
    {
        CreateMap<Tecnico, TecnicoResponse>().ReverseMap();
        CreateMap<CreateTecnicoRequest, Tecnico>().ReverseMap();
        CreateMap<Tecnico, CreateTecnicoResponse>().ReverseMap();
        CreateMap<UpdateTecnicoRequest, Tecnico>().ReverseMap();
        CreateMap<Tecnico, UpdateTecnicoResponse>().ReverseMap();
    }
}
