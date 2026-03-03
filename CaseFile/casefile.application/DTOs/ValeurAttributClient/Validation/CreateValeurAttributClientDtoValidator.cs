using FluentValidation;

namespace casefile.application.DTOs.ValeurAttributClient.Validation;

/// <summary>
/// Validateur du DTO de creation de valeur d'attribut client.
/// </summary>
public class CreateValeurAttributClientDtoValidator : AbstractValidator<CreateValeurAttributClientDto>
{
    public CreateValeurAttributClientDtoValidator()
    {
        RuleFor(x => x.Cle)
            .NotEmpty().WithMessage("La clé de la valeur d'attribut client est obligatoire.")
            .MaximumLength(100).WithMessage("La clé de la valeur d'attribut client ne peut pas dépasser 100 caractères.");
        RuleFor(x => x.Valeur)
            .NotEmpty().WithMessage("La valeur de l'attribut client est obligatoire.")
            .MaximumLength(4000).WithMessage("La valeur de l'attribut client ne peut pas dépasser 4000 caractères.");
    }
}
