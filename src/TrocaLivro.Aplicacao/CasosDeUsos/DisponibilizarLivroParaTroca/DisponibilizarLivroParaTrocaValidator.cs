using FluentValidation;
using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.CasosDeUsos.DisponibilizarLivroParaTroca
{
    public class DisponibilizarLivroParaTrocaValidator : AbstractValidator<DisponibilizarLivroParaTrocaViewModel>
    {
        public DisponibilizarLivroParaTrocaValidator()
        {
            RuleFor(e => e.Descritivo)
                .NotEmpty()
                .WithMessage("Informe o descritivo.");

            RuleFor(e => e.LivroId).NotEmpty()
                .Must(livroId => livroId > 0)
                .WithMessage("Informe o id do livro.");

            RuleFor(e => e.Pontos).NotEmpty()
                .WithMessage("Selecione a quantidade de pontos.");

            RuleFor(e => e.Pontos)
                .Must(pontos => pontos > 0 && pontos <= 3)
                .WithMessage("A quantidade de pontos deve ser maior que 0 e menor ou igual a 3.");
        }
    }
}