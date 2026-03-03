using FluentValidation;

namespace casefile.application.DTOs.DefinitionAttribut.Validation;

/// <summary>
/// Validateur du DTO de creation de definition d'attribut.
/// </summary>
public class CreateDefinitionAttributDtoValidator : AbstractValidator<CreateDefinitionAttributDto>
{
    public CreateDefinitionAttributDtoValidator()
    {
        RuleFor(x => x.Cle)
            .NotEmpty().WithMessage("La clé de l'attribut est obligatoire.")
            .MaximumLength(100).WithMessage("La clé de l'attribut ne peut pas dépasser 100 caractères.");
        RuleFor(x => x.Libelle)
            .NotEmpty().WithMessage("Le libellé de l'attribut est obligatoire.")
            .MaximumLength(200).WithMessage("Le libellé de l'attribut ne peut pas dépasser 200 caractères.");
        RuleFor(x => x.ValeurDefaut)
            .MaximumLength(1000)
            .WithMessage("La valeur par défaut de l'attribut ne peut pas dépasser 1000 caractères.")
            .When(x => string.IsNullOrWhiteSpace(x.ValeurDefaut) == false);
    }
}
