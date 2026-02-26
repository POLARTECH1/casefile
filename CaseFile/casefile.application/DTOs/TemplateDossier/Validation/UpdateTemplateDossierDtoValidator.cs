using casefile.application.DTOs.TemplateDossierElement;
using FluentValidation;

namespace casefile.application.DTOs.TemplateDossier.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de template dossier.
/// </summary>
public class UpdateTemplateDossierDtoValidator : AbstractValidator<UpdateTemplateDossierDto>
{
    public UpdateTemplateDossierDtoValidator(IValidator<CreateTemplateDossierElementDto> elementValidator)
    {
        Include(new CreateTemplateDossierDtoValidator(elementValidator));
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'identifiant du template de dossier est obligatoire.");
    }
}
