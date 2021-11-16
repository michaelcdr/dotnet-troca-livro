using AutoMapper;
using System.Linq;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Aplicacao.CasosDeUsos.EditarLivro;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.Mapping
{
    public class LivroProfile : Profile
    {
        public LivroProfile()
        {
            CreateMap<CadastrarLivroViewModel, CadastrarLivroCommand>();

            CreateMap<Livro, CadastrarLivroResultado>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Livro, LivroCardModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.Imagens, 
                           opt => opt.MapFrom(src => src.Imagens.Select(img => new ImagemCardLivro { Nome = img.Nome }).ToList())

                ).ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<CadastrarLivroCommand, Livro>()
                .ForMember(dest => dest.Autores,
                           opt => opt.MapFrom(
                                    source => source.AutorId.Select(autorId => new LivroAutor { AutorId = autorId }).ToList()
                               )
                           )
                .ForMember(dest => dest.Imagens,
                           opt => opt.Ignore());


            CreateMap<EditarLivroViewModel, EditarLivroCommand>();

            CreateMap<Livro, EditarLivroResultado>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
             

            CreateMap<ObterDadosDashboardResultado, ObterDadosDashboardViewModel>();
            CreateMap<ObterTrocaResultado, TrocarLivroViewModel>();
            CreateMap<TrocarLivroViewModel, SolicitarTrocaCommand>();
            CreateMap<ObterTrocaResultado,TrocaViewModel>();
            CreateMap<PacotePontos, ObterPacoteResultado>();
            CreateMap<ObterPacoteResultado, ConfirmarCompraPacoteViewModel>();
        }
    }
}
