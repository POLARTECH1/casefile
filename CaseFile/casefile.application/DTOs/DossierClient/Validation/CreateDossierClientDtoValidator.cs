using FluentValidation;

namespace casefile.application.DTOs.DossierClient.Validation;

/// <summary>
/// Validateur du DTO de creation de dossier client.
/// </summary>
public class CreateDossierClientDtoValidator : AbstractValidator<CreateDossierClientDto>
{
    public CreateDossierClientDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom du dossier client est obligatoire.")
            .MaximumLength(150).WithMessage("Le nom du dossier client ne peut pas dépasser 150 caractères.");
        RuleFor(x => x.CheminVirtuel)
            .NotEmpty().WithMessage("Le chemin virtuel du dossier client est obligatoire.")
            .MaximumLength(500).WithMessage("Le chemin virtuel du dossier client ne peut pas dépasser 500 caractères.");
    }
}
