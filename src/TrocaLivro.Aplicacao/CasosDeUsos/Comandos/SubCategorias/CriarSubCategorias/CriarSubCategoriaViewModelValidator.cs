using FluentValidation;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarSubCategoriaViewModelValidator : AbstractValidator<CriarSubCategoriaViewModel>
    {
        public CriarSubCategoriaViewModelValidator()
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