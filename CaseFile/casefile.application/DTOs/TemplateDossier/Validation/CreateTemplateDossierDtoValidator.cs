using casefile.application.DTOs.TemplateDossierElement;
using FluentValidation;

namespace casefile.application.DTOs.TemplateDossier.Validation;

/// <summary>
/// Validateur du DTO de creation de template dossier.
/// </summary>
public class CreateTemplateDossierDtoValidator : AbstractValidator<CreateTemplateDossierDto>
{
    public CreateTemplateDossierDtoValidator(IValidator<CreateTemplateDossierElementDto> elementValidator)
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Description).MaximumLength(2000).When(x => string.IsNullOrWhiteSpace(x.Description) == false);
        RuleFor(x => x.CreateTemplateDossierElementDtos).NotNull();
        RuleFor(x => x.CreateTemplateDossierElementDtos)
            .Must(list => list.Count > 0)
            .WithMessage("Le template doit contenir au moins un élément.");
        RuleForEach(x => x.CreateTemplateDossierElementDtos)
            .SetValidator(elementValidator);
        RuleFor(x => x.CreateTemplateDossierElementDtos)
            .Must(list => list.Select(e => e.Ordre).Distinct().Count() == list.Count)
            .WithMessage("Chaque élément doit avoir un ordre unique.");

    }
}