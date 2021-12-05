using FluentValidation;
using TrocaLivro.Aplicacao.CasosDeUsos;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarAutorCommandValidator : AbstractValidator<CriarAutorCommand>
    {
        public CriarAutorCommandValidator()
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