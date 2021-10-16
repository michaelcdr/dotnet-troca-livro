using System;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.ViewModels
{
    public class AvaliacaoLivro
    {
        public AvaliacaoLivro(string titulo, string descricao, NotaLivroEnum nota, DateTime data, string usuario)
        {
            Titulo = titulo;
            Descricao = descricao;
            Nota = nota;
            Data = data.ToString("dd/MM/yyyy HH:mm:ss");
            Usuario = usuario;  
        }
        public AvaliacaoLivro()
        {

        }
        public string Data { get;  set; }
        public string Usuario { get;  set; }
        public string Titulo { get;  set; }
        public string Descricao { get;  set; }
        public NotaLivroEnum Nota { get;  set; }
    }
}
