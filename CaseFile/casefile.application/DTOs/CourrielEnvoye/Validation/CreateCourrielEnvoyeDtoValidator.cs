using FluentValidation;

namespace casefile.application.DTOs.CourrielEnvoye.Validation;

/// <summary>
/// Validateur du DTO de creation de courriel envoye.
/// </summary>
public class CreateCourrielEnvoyeDtoValidator : AbstractValidator<CreateCourrielEnvoyeDto>
{
    public CreateCourrielEnvoyeDtoValidator()
    {
        RuleFor(x => x.A)
            .NotEmpty().WithMessage("Le destinataire du courriel est obligatoire.")
            .EmailAddress().WithMessage("L'adresse email du destinataire n'est pas valide.")
            .MaximumLength(320).WithMessage("L'adresse email du destinataire ne peut pas dépasser 320 caractères.");
        RuleFor(x => x.Sujet)
            .NotEmpty().WithMessage("Le sujet du courriel est obligatoire.")
            .MaximumLength(200).WithMessage("Le sujet du courriel ne peut pas dépasser 200 caractères.");
        RuleFor(x => x.PiecesJointes)
            .NotEmpty().WithMessage("Les pièces jointes sont obligatoires.")
            .MaximumLength(2000).WithMessage("La liste des pièces jointes ne peut pas dépasser 2000 caractères.");
        RuleFor(x => x.EnvoyeLe)
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1))
            .WithMessage("La date d'envoi du courriel ne peut pas être dans le futur.");
    }
}
