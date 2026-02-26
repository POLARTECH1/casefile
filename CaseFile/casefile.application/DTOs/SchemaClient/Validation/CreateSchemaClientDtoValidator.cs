using FluentValidation;
using casefile.application.DTOs.DefinitionAttribut;

namespace casefile.application.DTOs.SchemaClient.Validation;

/// <summary>
/// Validateur du DTO de creation de schema client.
/// </summary>
public class CreateSchemaClientDtoValidator : AbstractValidator<CreateSchemaClientDto>
{
    public CreateSchemaClientDtoValidator(IValidator<CreateDefinitionAttributDto> definitionAttributValidator)
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom du schéma client est obligatoire.")
            .MaximumLength(150).WithMessage("Le nom du schéma client ne peut pas dépasser 150 caractères.");

        RuleFor(x => x.CreateDefinitionAttributDtos)
            .NotNull()
            .WithMessage("La liste des propriétés ne peut pas être nulle.");

        RuleFor(x => x.CreateDefinitionAttributDtos)
            .Must(list => list.Count > 0)
            .WithMessage("Le schéma doit contenir au moins une propriété.");

        RuleForEach(x => x.CreateDefinitionAttributDtos)
            .SetValidator(definitionAttributValidator);

        RuleFor(x => x.CreateDefinitionAttributDtos)
            .Must(list => list
                .Select(d => d.Cle.Trim().ToLowerInvariant())
                .Distinct()
                .Count() == list.Count)
            .WithMessage("Chaque propriété doit avoir une clé unique.");
    }
}
