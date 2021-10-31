using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{

    public class PacotePontosViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descritivo { get; set; }
        public decimal Valor { get; set; }
        public int Pontos { get; set; }

        public string ObterValorFormatado()
        {
            return Valor.ToString("C", CultureInfo.CurrentCulture);
        }
    }

}
