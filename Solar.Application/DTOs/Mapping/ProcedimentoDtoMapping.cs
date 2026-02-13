using AutoMapper;
using Solar.Application.DTOs.Procedimento;

namespace Solar.Application.DTOs.Mapping;
using Solar.Domain.Entities;

public class ProcedimentoDtoMapping : Profile
{
    public ProcedimentoDtoMapping()
    {
        CreateMap<Procedimento, ProcedimentoResponse>().ReverseMap();
        CreateMap<CreateProcedimentoRequest, Procedimento>().ReverseMap();
        CreateMap<Procedimento, CreateProcedimentoResponse>().ReverseMap();
        CreateMap<UpdateProcedimentoRequest, Procedimento>().ReverseMap();
        CreateMap<Procedimento, UpdateProcedimentoResponse>().ReverseMap();
    }
}
