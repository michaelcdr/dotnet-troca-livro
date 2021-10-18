using FluentValidation;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class AvaliarLivroValidator : AbstractValidator<AvaliarLivroViewModel>
    {
        public AvaliarLivroValidator()
        {
            RuleFor(model => model.Titulo)
                .NotEmpty()
                .WithMessage("Informe o título")
                .MinimumLength(10)
                .WithMessage("Escreva um título com ao menos 10 caracteres.");

            RuleFor(model => model.Descricao)
                .NotEmpty()
                .WithMessage("Informe uma descrição");

            //RuleFor(model => model.Nota)
            //    .NotEmpty()
            //    .WithMessage("A nota deve ser selecionada.");

            RuleFor(model => model.Nota)
                .IsInEnum()
                .WithMessage("A nota deve ser selecionada.");
        }
    }
}