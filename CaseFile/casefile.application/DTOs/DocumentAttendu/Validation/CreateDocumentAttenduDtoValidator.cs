using FluentValidation;

namespace casefile.application.DTOs.DocumentAttendu.Validation;

/// <summary>
/// Validateur du DTO de creation de document attendu.
/// </summary>
public class CreateDocumentAttenduDtoValidator : AbstractValidator<CreateDocumentAttenduDto>
{
    public CreateDocumentAttenduDtoValidator()
    {
        RuleFor(x => x.IdTypeDocument)
            .NotNull()
            .WithMessage("Le type de document attendu est obligatoire.");
    }
}
