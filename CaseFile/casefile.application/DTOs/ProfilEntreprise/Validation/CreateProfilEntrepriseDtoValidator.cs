using FluentValidation;

namespace casefile.application.DTOs.ProfilEntreprise.Validation;

/// <summary>
/// Validateur du DTO de creation de profil entreprise.
/// </summary>
public class CreateProfilEntrepriseDtoValidator : AbstractValidator<CreateProfilEntrepriseDto>
{
    public CreateProfilEntrepriseDtoValidator()
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(320);
        RuleFor(x => x.Telephone).MaximumLength(50).When(x => string.IsNullOrWhiteSpace(x.Telephone) == false);
        RuleFor(x => x.Adresse).MaximumLength(500).When(x => string.IsNullOrWhiteSpace(x.Adresse) == false);
        RuleFor(x => x.LogoPath).MaximumLength(1000).When(x => string.IsNullOrWhiteSpace(x.LogoPath) == false);
        RuleFor(x => x.Signature).MaximumLength(4000).When(x => string.IsNullOrWhiteSpace(x.Signature) == false);
    }
}
