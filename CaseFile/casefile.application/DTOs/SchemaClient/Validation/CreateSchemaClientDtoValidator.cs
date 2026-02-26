using FluentValidation;

namespace casefile.application.DTOs.SchemaClient.Validation;

/// <summary>
/// Validateur du DTO de creation de schema client.
/// </summary>
public class CreateSchemaClientDtoValidator : AbstractValidator<CreateSchemaClientDto>
{
    public CreateSchemaClientDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom du schéma client est obligatoire.")
            .MaximumLength(150).WithMessage("Le nom du schéma client ne peut pas dépasser 150 caractères.");
    }
}
