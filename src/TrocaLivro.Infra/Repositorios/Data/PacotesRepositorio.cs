using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Repositorios.Data
{
    public class PacotesRepositorio
    {
        private readonly List<PacotePontos> _pacotes;

        public PacotesRepositorio()
        {
            this._pacotes = new List<PacotePontos>
            {
                new PacotePontos(1, "Pacote básico", "10 Pontos", 50.0m, 10),
                new PacotePontos(2, "Pacote plus ", "20 Pontos", 100.0m, 20),
                new PacotePontos(3, "Pacote master", "30 Pontos", 130.0m, 30)
            };
        }
         
        public PacotePontos Obter(int id)
        {
            return this._pacotes.Single(e => e.Id == id);
        }

        public List<PacotePontos> ObterTodos()
        {
            return this._pacotes;  
        }
    }
}
