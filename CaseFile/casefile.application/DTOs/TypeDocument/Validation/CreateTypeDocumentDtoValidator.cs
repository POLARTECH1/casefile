using FluentValidation;

namespace casefile.application.DTOs.TypeDocument.Validation;

/// <summary>
/// Validateur du DTO de creation de type de document.
/// </summary>
public class CreateTypeDocumentDtoValidator : AbstractValidator<CreateTypeDocumentDto>
{
    public CreateTypeDocumentDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Le code du type de document est obligatoire.")
            .MaximumLength(50).WithMessage("Le code du type de document ne peut pas dépasser 50 caractères.");
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom du type de document est obligatoire.")
            .MaximumLength(150).WithMessage("Le nom du type de document ne peut pas dépasser 150 caractères.");
        RuleFor(x => x.ExtensionsPermises)
            .MaximumLength(500)
            .WithMessage("Les extensions permises ne peuvent pas dépasser 500 caractères.")
            .When(x => string.IsNullOrWhiteSpace(x.ExtensionsPermises) == false);
        RuleFor(x => x.DossierCibleParDefaut)
            .MaximumLength(500)
            .WithMessage("Le dossier cible par défaut ne peut pas dépasser 500 caractères.")
            .When(x => string.IsNullOrWhiteSpace(x.DossierCibleParDefaut) == false);
    }
}
