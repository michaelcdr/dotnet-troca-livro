using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class DeletarLivroCommand : IRequest<AppResponse<DeletarLivroResultado>>
    {
        public int LivroId { get; set; }
        public string Usuario { get; set; }
        public DeletarLivroCommand(int livroId, string usuario )
        {
            this.LivroId = livroId;
            this.Usuario = usuario;
        }
    }
}
