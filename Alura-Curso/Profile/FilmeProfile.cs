using Alura_Curso.Data.Dtos;
using AutoMapper;
using Alura_Curso.Models;

namespace Alura_Curso.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filmes>();
        CreateMap<UpdateFilmeDto, Filmes>();
        CreateMap<Filmes, UpdateFilmeDto>();
    }
}