using FluentValidation;

namespace casefile.application.DTOs.TypeDocument.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de type de document.
/// </summary>
public class UpdateTypeDocumentDtoValidator : AbstractValidator<UpdateTypeDocumentDto>
{
    public UpdateTypeDocumentDtoValidator()
    {
        Include(new CreateTypeDocumentDtoValidator());
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'identifiant du type de document est obligatoire.");
    }
}
