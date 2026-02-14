using FluentValidation;

namespace casefile.application.DTOs.TypeDocument.Validation;

/// <summary>
/// Validateur du DTO de creation de type de document.
/// </summary>
public class CreateTypeDocumentDtoValidator : AbstractValidator<CreateTypeDocumentDto>
{
    public CreateTypeDocumentDtoValidator()
    {
        RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(150);
        RuleFor(x => x.ExtensionsPermises).MaximumLength(500).When(x => string.IsNullOrWhiteSpace(x.ExtensionsPermises) == false);
        RuleFor(x => x.DossierCibleParDefaut).MaximumLength(500).When(x => string.IsNullOrWhiteSpace(x.DossierCibleParDefaut) == false);
    }
}
