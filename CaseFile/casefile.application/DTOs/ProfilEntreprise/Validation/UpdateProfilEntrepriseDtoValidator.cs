using FluentValidation;

namespace casefile.application.DTOs.ProfilEntreprise.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de profil entreprise.
/// </summary>
public class UpdateProfilEntrepriseDtoValidator : AbstractValidator<UpdateProfilEntrepriseDto>
{
    public UpdateProfilEntrepriseDtoValidator()
    {
        Include(new CreateProfilEntrepriseDtoValidator());
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'identifiant du profil entreprise est obligatoire.");
    }
}
