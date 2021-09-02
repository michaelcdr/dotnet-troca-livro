using AutoMapper;
using TrocaLivro.Aplicacao.CasosDeUsos;

namespace TrocaLivro.Aplicacao.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<RegistrarUsuarioModel, RegistrarUsuarioCommand>();
        }
    }
}
