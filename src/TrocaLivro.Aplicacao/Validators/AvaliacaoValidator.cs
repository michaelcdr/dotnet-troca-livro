using FluentValidation;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.Validators
{
    public class AvaliacaoValidator:AbstractValidator<Avaliacao>
    {
        public AvaliacaoValidator()
        {
            RuleFor(model => model.Titulo)
                .NotEmpty()
                .WithMessage("Informe o título");

            RuleFor(model => model.Titulo)
                .Must(e => e.Length >= 10)
                .WithMessage("Escreva um título com ao menos 10 caracteres.");

            RuleFor(model => model.Descricao)
                .NotEmpty()
                .WithMessage("Informe uma descrição");

            RuleFor(model => model.Nota)
                .NotEmpty()
                .Must(e => e.GetHashCode() > 0 && e.GetHashCode() <= 5)
                .WithMessage("A nota deve ser selecionada.");
        }
    }

    public class CadastroAutorViewModelValidator : AbstractValidator<CadastroAutorViewModel>
    {
        public CadastroAutorViewModelValidator()
        {
            RuleFor(model => model.Nome)
                .NotEmpty()
                .WithMessage("Informe o Nome");

            RuleFor(model => model.Nome)
                .Must(e => e.Length <= 100)
                .WithMessage("Escreva um nome no máximo 100 caracteres.");
        }
    }

    public class CadastroCategoriaViewModelValidator : AbstractValidator<CadastroCategoriaViewModel>
    {
        public CadastroCategoriaViewModelValidator()
        {
            RuleFor(model => model.Nome)
                .NotEmpty()
                .WithMessage("Informe o Nome");

            RuleFor(model => model.Nome)
                .Must(e => e.Length <= 100)
                .WithMessage("Escreva um nome no máximo 100 caracteres.");
        }
    }

    

    public class CadastroEditoraViewModelValidator : AbstractValidator<CadastroEditoraViewModel>
    {
        public CadastroEditoraViewModelValidator()
        {
            RuleFor(model => model.Nome)
                .NotEmpty()
                .WithMessage("Informe o Nome");

            RuleFor(model => model.Nome)
                .Must(e => e.Length <= 100)
                .WithMessage("Escreva um nome no máximo 100 caracteres.");
        }
    }
}