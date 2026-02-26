using FluentValidation;

namespace casefile.application.DTOs.TemplateCourriel.Validation;

/// <summary>
/// Validateur du DTO de creation de template courriel.
/// </summary>
public class CreateTemplateCourrielDtoValidator : AbstractValidator<CreateTemplateCourrielDto>
{
    public CreateTemplateCourrielDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom du template de courriel est obligatoire.")
            .MaximumLength(150).WithMessage("Le nom du template de courriel ne peut pas dépasser 150 caractères.");
        RuleFor(x => x.Sujet)
            .NotEmpty().WithMessage("Le sujet du template de courriel est obligatoire.")
            .MaximumLength(200).WithMessage("Le sujet du template de courriel ne peut pas dépasser 200 caractères.");
        RuleFor(x => x.Corps)
            .NotEmpty().WithMessage("Le corps du template de courriel est obligatoire.")
            .MaximumLength(20000).WithMessage("Le corps du template de courriel ne peut pas dépasser 20000 caractères.");
    }
}
