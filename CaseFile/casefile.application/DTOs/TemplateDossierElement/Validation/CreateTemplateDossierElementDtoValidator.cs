using casefile.application.DTOs.DocumentAttendu.Validation;
using FluentValidation;

namespace casefile.application.DTOs.TemplateDossierElement.Validation;

/// <summary>
/// Validateur du DTO de creation d'élément de template dossier.
/// </summary>
public class CreateTemplateDossierElementDtoValidator : AbstractValidator<CreateTemplateDossierElementDto>
{
    public CreateTemplateDossierElementDtoValidator()
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Ordre).GreaterThanOrEqualTo(0);
        RuleForEach(x => x.CreateDocumentAttendusDto).SetValidator(new CreateDocumentAttenduDtoValidator());
        RuleFor(x => x.CreateDocumentAttendusDto)
            .Must(list => list.Select(d => d.IdTypeDocument).Distinct().Count() == list.Count)
            .WithMessage("Chaque document attendu doit avoir un type de document unique.");
    }
}