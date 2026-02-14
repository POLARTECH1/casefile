using FluentValidation;

namespace casefile.application.DTOs.RegleNommageDocument.Validation;

/// <summary>
/// Validateur du DTO de creation de regle de nommage de document.
/// </summary>
public class CreateRegleNommageDocumentDtoValidator : AbstractValidator<CreateRegleNommageDocumentDto>
{
    public CreateRegleNommageDocumentDtoValidator()
    {
        RuleFor(x => x.Pattern).NotEmpty().MaximumLength(500);
    }
}
