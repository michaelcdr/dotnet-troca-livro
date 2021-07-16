using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class LivroCadastro
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int EditoraId { get; set; }
        public List<int> AutorId { get; set; }
        public int ISBN { get; set; }
        public int Ano { get; set; }
        public int NumeroPaginas { get; set; }
        public string Subtitulo { get; set; }
        public int CategoriaId { get; set; }
    }
}
