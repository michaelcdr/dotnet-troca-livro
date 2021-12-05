using FluentValidation;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class EditarUsuarioCommandValidator : AbstractValidator<EditarUsuarioCommand>
    { 
        public EditarUsuarioCommandValidator()
        {
            RuleFor(model => model.Nome)
                .NotEmpty()
                .WithMessage("Informe o nome");

            RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage("Informe o e-mail");

            RuleFor(model => model.Sobrenome)
                .NotEmpty()
                .WithMessage("Informe o sobrenome.");
        }
    }
}
