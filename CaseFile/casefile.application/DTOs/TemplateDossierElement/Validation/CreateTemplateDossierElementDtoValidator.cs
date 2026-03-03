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
        RuleFor(x => x.Nom)
            .NotEmpty()
            .WithMessage("Le nom de l'élément de template est obligatoire.")
            .MaximumLength(150)
            .WithMessage("Le nom de l'élément de template ne peut pas dépasser 150 caractères.");
        RuleFor(x => x.Ordre)
            .GreaterThanOrEqualTo(0)
            .WithMessage("L'ordre de l'élément de template doit être supérieur ou égal à 0.");
        RuleForEach(x => x.CreateDocumentAttendusDto).SetValidator(new CreateDocumentAttenduDtoValidator());
        RuleFor(x => x.CreateDocumentAttendusDto)
            .Must(list => list.Select(d => d.IdTypeDocument).Distinct().Count() == list.Count)
            .WithMessage("Chaque document attendu doit avoir un type de document unique.");
    }
}
