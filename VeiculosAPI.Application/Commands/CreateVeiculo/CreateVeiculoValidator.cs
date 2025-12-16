using FluentValidation;

namespace VeiculosAPI.Application.Commands.CreateVeiculo;

public class CreateVeiculoValidator : AbstractValidator<CreateVeiculoCommand>
{
    public CreateVeiculoValidator()
    {
        RuleFor(v => v.Descricao)
            .NotEmpty().WithMessage("A Descrição é obrigatória.")
            .MaximumLength(100).WithMessage("A Descrição deve ter no máximo 100 caracteres.");

        RuleFor(v => v.Modelo)
            .NotEmpty().WithMessage("O Modelo é obrigatório.");

        RuleFor(v => v.Marca)
            .IsInEnum().WithMessage("A Marca informada não é válida.");

        RuleFor(v => v.Valor)
            .GreaterThan(0).When(v => v.Valor.HasValue)
            .WithMessage("O Valor deve ser maior que zero.");
    }
}