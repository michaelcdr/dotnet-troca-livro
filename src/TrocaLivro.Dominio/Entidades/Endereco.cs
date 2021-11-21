using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Endereco : EntidadeBase
    {
        public int Id { get; set; }
        
        public string UsuarioId { get; private set; }
        public Usuario Usuario { get; set; }

        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Complemento { get; private set; }
        public string UF { get; private set; }
        public string Logradouro { get; private set; }
        public int Numero { get; private set; }
        public string Cidade { get; private set; }
        public List<Troca> Trocas { get; private set; }

        public Endereco(
            string usuarioId, string bairro, string cEP, string complemento, 
            string uF, string logradouro, int numero, string cidade)
        {
            this.UsuarioId = usuarioId;
            this.Bairro = bairro;
            this.CEP = cEP; 
            this.Complemento = complemento;
            this.UF = uF;   
            this.Logradouro = logradouro;   
            this.Numero = numero;
            this.Cidade = cidade;
            this.Trocas = new List<Troca>();
        }

        public override bool TaValido()
        {
            return this.UsuarioId != null &&
                !string.IsNullOrEmpty(this.Bairro) &&
                !string.IsNullOrEmpty(this.CEP) &&
                !string.IsNullOrEmpty(this.UF) &&
                !string.IsNullOrEmpty(this.Cidade) &&
                !string.IsNullOrEmpty(this.Logradouro) &&
                this.Numero > 0;
        }
    }
}
