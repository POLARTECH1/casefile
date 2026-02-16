using FluentValidation;

namespace casefile.application.DTOs.DefinitionAttribut.Validation;

/// <summary>
/// Validateur du DTO de creation de definition d'attribut.
/// </summary>
public class CreateDefinitionAttributDtoValidator : AbstractValidator<CreateDefinitionAttributDto>
{
    public CreateDefinitionAttributDtoValidator()
    {
        RuleFor(x => x.Cle).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Libelle).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ValeurDefaut).MaximumLength(1000).When(x => string.IsNullOrWhiteSpace(x.ValeurDefaut) == false);
    }
}
