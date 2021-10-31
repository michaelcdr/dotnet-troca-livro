using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ConfirmarCompraPacoteViewModel
    {
        public ConfirmarCompraPacoteViewModel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descritivo { get; set; }
        public decimal Valor { get; set; }
        public int Pontos { get; set; }
    }
}
