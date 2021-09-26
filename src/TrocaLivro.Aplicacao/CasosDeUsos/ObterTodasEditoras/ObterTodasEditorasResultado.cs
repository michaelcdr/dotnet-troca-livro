using System.Collections.Generic;
using TrocaLivro.Aplicacao.DTO;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTodasEditorasResultado
    {
        public ObterTodasEditorasResultado(List<EditoraDTO> editoras)
        {
            Editoras = editoras;
        }

        public List<EditoraDTO> Editoras { get; set; }
    }
}
