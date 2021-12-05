using MediatR;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class AvaliarLivroCommand : IRequest<AppResponse<AvaliarLivroResultado>>
    {
        public int LivroId { get; set; }
        public string Usuario { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public NotaLivroEnum Nota { get; set; }
    }
}
