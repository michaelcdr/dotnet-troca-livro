using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class TrocarLivroViewModel
    {
        public TrocarLivroViewModel()
        {
             
        }
        public int TrocaId { get; set; }
        public int Pontos { get;  set; }
        public StatusTroca Status { get;  set; }
        public string Descritivo { get;  set; }
        public string DisponibilizadoPor { get;  set; }
        public int LivroId { get;  set; }
        public string TituloLivro { get;  set; }
        public DateTime DisponibilizadoEm { get;  set; }
        public string Capa { get; set; }
        public string TituloDestaque 
        { 
            get { 
                return $"Você está prestes a obter o livro <strong>{this.TituloLivro}</strong> por "+
                       $"<strong>{(this.Pontos > 1 ? this.Pontos + " pontos" : this.Pontos + " ponto")}</strong>."; 
            }
        }
        
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}")]
        public int? Numero { get; set; }
        
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}")]
        public string UF { get; set; }

        public List<SelectListItem> Estados { get; set; }
    } 
}