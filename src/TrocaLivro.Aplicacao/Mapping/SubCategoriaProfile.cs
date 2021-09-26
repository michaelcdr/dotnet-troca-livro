using AutoMapper;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.Mapping
{
    public class SubCategoriaProfile : Profile
    {
        public SubCategoriaProfile()
        {
            CreateMap<SubCategoria, SubCategoriaDTO>().ForMember(dest => dest.NomeCategoria,
                opt => opt.MapFrom(source => source.Categoria.Nome));
        }
    }
}
