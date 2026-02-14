using FluentValidation;

namespace casefile.application.DTOs.DocumentClient.Validation;

/// <summary>
/// Validateur du DTO de creation de document client.
/// </summary>
public class CreateDocumentClientDtoValidator : AbstractValidator<CreateDocumentClientDto>
{
    public CreateDocumentClientDtoValidator()
    {
        RuleFor(x => x.NomOriginal).NotEmpty().MaximumLength(255);
        RuleFor(x => x.NomStandardise).NotEmpty().MaximumLength(255);
        RuleFor(x => x.CheminPhysique).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Extension).NotEmpty().MaximumLength(20);
        RuleFor(x => x.AjouteLe).LessThanOrEqualTo(DateTime.UtcNow.AddDays(1));
    }
}
