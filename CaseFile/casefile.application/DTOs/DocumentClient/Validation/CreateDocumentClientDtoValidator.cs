using FluentValidation;

namespace casefile.application.DTOs.DocumentClient.Validation;

/// <summary>
/// Validateur du DTO de creation de document client.
/// </summary>
public class CreateDocumentClientDtoValidator : AbstractValidator<CreateDocumentClientDto>
{
    public CreateDocumentClientDtoValidator()
    {
        RuleFor(x => x.NomOriginal)
            .NotEmpty().WithMessage("Le nom original du document est obligatoire.")
            .MaximumLength(255).WithMessage("Le nom original du document ne peut pas dépasser 255 caractères.");
        RuleFor(x => x.NomStandardise)
            .NotEmpty().WithMessage("Le nom standardisé du document est obligatoire.")
            .MaximumLength(255).WithMessage("Le nom standardisé du document ne peut pas dépasser 255 caractères.");
        RuleFor(x => x.CheminPhysique)
            .NotEmpty().WithMessage("Le chemin physique du document est obligatoire.")
            .MaximumLength(1000).WithMessage("Le chemin physique du document ne peut pas dépasser 1000 caractères.");
        RuleFor(x => x.Extension)
            .NotEmpty().WithMessage("L'extension du document est obligatoire.")
            .MaximumLength(20).WithMessage("L'extension du document ne peut pas dépasser 20 caractères.");
        RuleFor(x => x.AjouteLe)
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1))
            .WithMessage("La date d'ajout du document ne peut pas être dans le futur.");
    }
}
