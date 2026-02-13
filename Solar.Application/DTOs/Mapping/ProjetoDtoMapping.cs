using AutoMapper;
using Solar.Application.DTOs.Projeto;

namespace Solar.Application.DTOs.Mapping;
using Solar.Domain.Entities;

public class ProjetoDtoMapping : Profile
{
    public ProjetoDtoMapping()
    {
        CreateMap<Projeto, ProjetoResponse>().ReverseMap();
        CreateMap<CreateProjetoRequest, Projeto>().ReverseMap();
        CreateMap<Projeto, CreateProjetoResponse>().ReverseMap();
        CreateMap<UpdateProjetoRequest, Projeto>().ReverseMap();
        CreateMap<Projeto, UpdateProjetoResponse>().ReverseMap();
    }
}
