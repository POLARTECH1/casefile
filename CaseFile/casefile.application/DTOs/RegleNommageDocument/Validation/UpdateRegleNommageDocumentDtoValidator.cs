using FluentValidation;

namespace casefile.application.DTOs.RegleNommageDocument.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de regle de nommage de document.
/// </summary>
public class UpdateRegleNommageDocumentDtoValidator : AbstractValidator<UpdateRegleNommageDocumentDto>
{
    public UpdateRegleNommageDocumentDtoValidator()
    {
        Include(new CreateRegleNommageDocumentDtoValidator());
        RuleFor(x => x.Id).NotEmpty();
    }
}
