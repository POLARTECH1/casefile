using FluentValidation;

namespace casefile.application.DTOs.ProfilEntreprise.Validation;

/// <summary>
/// Validateur du DTO de creation de profil entreprise.
/// </summary>
public class CreateProfilEntrepriseDtoValidator : AbstractValidator<CreateProfilEntrepriseDto>
{
    public CreateProfilEntrepriseDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom de l'entreprise est obligatoire.")
            .MaximumLength(200).WithMessage("Le nom de l'entreprise ne peut pas dépasser 200 caractères.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'email de l'entreprise est obligatoire.")
            .EmailAddress().WithMessage("L'email de l'entreprise n'est pas valide.")
            .MaximumLength(320).WithMessage("L'email de l'entreprise ne peut pas dépasser 320 caractères.");
        RuleFor(x => x.Telephone)
            .MaximumLength(50)
            .WithMessage("Le téléphone de l'entreprise ne peut pas dépasser 50 caractères.")
            .When(x => string.IsNullOrWhiteSpace(x.Telephone) == false);
        RuleFor(x => x.Adresse)
            .MaximumLength(500)
            .WithMessage("L'adresse de l'entreprise ne peut pas dépasser 500 caractères.")
            .When(x => string.IsNullOrWhiteSpace(x.Adresse) == false);
        RuleFor(x => x.LogoPath)
            .MaximumLength(1000)
            .WithMessage("Le chemin du logo ne peut pas dépasser 1000 caractères.")
            .When(x => string.IsNullOrWhiteSpace(x.LogoPath) == false);
        RuleFor(x => x.Signature)
            .MaximumLength(4000)
            .WithMessage("La signature de l'entreprise ne peut pas dépasser 4000 caractères.")
            .When(x => string.IsNullOrWhiteSpace(x.Signature) == false);
    }
}
