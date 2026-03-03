using FluentValidation;

namespace casefile.application.DTOs.DocumentClient.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de document client.
/// </summary>
public class UpdateDocumentClientDtoValidator : AbstractValidator<UpdateDocumentClientDto>
{
    public UpdateDocumentClientDtoValidator()
    {
        Include(new CreateDocumentClientDtoValidator());
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'identifiant du document client est obligatoire.");
    }
}
