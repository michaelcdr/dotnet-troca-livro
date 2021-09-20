using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrocaLivro.Dominio.DTO;

namespace TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro
{
    public class CadastrarLivroViewModel
    {
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Titulo { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Descricao { get; set; }

        [Display(Name = "Editora")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public int? EditoraId { get; set; }
        public List<int> AutorId { get; set; }

        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string ISBN { get; set; }

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public int? Ano { get; set; }
        
        [Display(Name = "Número de páginas")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public int? NumeroPaginas { get; set; }

        [Display(Name = "Subtitulo")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string Subtitulo { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public int? CategoriaId { get; set; }

        [Display(Name = "SubCategoria")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public int? SubCategoriaId { get; set; }

        public List<IFormFile> Imagens { get; set; }

        public List<AutorDTO> Autores { get; set; }
        public List<EditoraDTO> Editoras { get; set; }
        public List<CategoriaDTO> Categorias { get; set; }
        public string Usuario { get; set; }
        public string Token { get; set; }

        public CadastrarLivroViewModel()
        {

        }

        public CadastrarLivroViewModel(List<EditoraDTO> editoras, List<AutorDTO> autores, List<CategoriaDTO> categorias)
        {
            this.Editoras = editoras;
            this.Autores = autores;
            this.Categorias = categorias;
        }
    }
}
