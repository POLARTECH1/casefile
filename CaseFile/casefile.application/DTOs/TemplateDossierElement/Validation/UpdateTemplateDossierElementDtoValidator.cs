using FluentValidation;

namespace casefile.application.DTOs.TemplateDossierElement.Validation;

/// <summary>
/// Validateur du DTO de mise a jour d'élément de template dossier.
/// </summary>
public class UpdateTemplateDossierElementDtoValidator : AbstractValidator<UpdateTemplateDossierElementDto>
{
    public UpdateTemplateDossierElementDtoValidator()
    {
        Include(new CreateTemplateDossierElementDtoValidator());
        RuleFor(x => x.Id).NotEmpty();
    }
}
