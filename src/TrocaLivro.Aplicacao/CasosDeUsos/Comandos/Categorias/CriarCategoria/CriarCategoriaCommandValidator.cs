﻿using FluentValidation;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarCategoriaCommandValidator : AbstractValidator<CriarCategoriaCommand>
    {
        public CriarCategoriaCommandValidator()
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
