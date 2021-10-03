using System.Collections.Generic;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public interface IEdicaoCadastroLivroCommand
    {
        int Id { get; set; }
        string Titulo { get; set; }

        string Descricao { get; set; }

        int? EditoraId { get; set; }
        List<int> AutorId { get; set; }

        string ISBN { get; set; }

        int? Ano { get; set; }

        int? NumeroPaginas { get; set; }

        string Subtitulo { get; set; }

        int? SubCategoriaId { get; set; }
        string Usuario { get; set; }
    }
}
