using FluentValidation;

namespace casefile.application.DTOs.SchemaClient.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de schema client.
/// </summary>
public class UpdateSchemaClientDtoValidator : AbstractValidator<UpdateSchemaClientDto>
{
    public UpdateSchemaClientDtoValidator()
    {
        Include(new CreateSchemaClientDtoValidator());
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'identifiant du schéma client est obligatoire.");
    }
}
