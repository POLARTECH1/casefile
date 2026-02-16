using FluentValidation;

namespace casefile.application.DTOs.DocumentAttendu.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de document attendu.
/// </summary>
public class UpdateDocumentAttenduDtoValidator : AbstractValidator<UpdateDocumentAttenduDto>
{
    public UpdateDocumentAttenduDtoValidator()
    {
        Include(new CreateDocumentAttenduDtoValidator());
        RuleFor(x => x.Id).NotEmpty();
    }
}
