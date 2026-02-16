using FluentValidation;

namespace casefile.application.DTOs.SchemaClient.Validation;

/// <summary>
/// Validateur du DTO de creation de schema client.
/// </summary>
public class CreateSchemaClientDtoValidator : AbstractValidator<CreateSchemaClientDto>
{
    public CreateSchemaClientDtoValidator()
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(150);
    }
}
