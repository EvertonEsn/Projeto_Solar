using AutoMapper;
using Solar.Application.DTOs.Cliente;

namespace Solar.Application.DTOs.Mapping;
using Solar.Domain.Entities;

public class ClienteDtoMapping : Profile
{
    public ClienteDtoMapping()
    {
        CreateMap<Cliente, ClienteResponse>().ReverseMap();
        CreateMap<CreateClienteRequest, Cliente>().ReverseMap();
        CreateMap<Cliente, CreateClienteResponse>().ReverseMap();
        CreateMap<Cliente, UpdateClienteRequest>().ReverseMap();
        CreateMap<Cliente, UpdateClienteResponse>().ReverseMap();
    }
}