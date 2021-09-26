using System.Collections.Generic;
using TrocaLivro.Aplicacao.DTO;

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterTodosAutores
{
    public class ObterTodosAutoresResultado
    {
        public ObterTodosAutoresResultado(List<AutorDTO> autores)
        {
            Autores = autores;
        }

        public List<AutorDTO> Autores { get; set; }
    }
}