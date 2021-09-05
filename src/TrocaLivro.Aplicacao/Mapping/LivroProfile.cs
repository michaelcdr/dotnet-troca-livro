using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.Mapping
{
    public class LivroProfile : Profile
    {
        public LivroProfile()
        {
            CreateMap<CadastrarLivroViewModel, CadastrarLivroCommand>();
            CreateMap<CadastrarLivroCommand, Livro>()
                .ForMember(dest => dest.Autores, 
                           opt => opt.MapFrom(
                                    source => source.AutorId.Select(autorId => new LivroAutor { AutorId = autorId }).ToList()
                               )
                           );

            CreateMap<Livro, CadastrarLivroResultado>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
