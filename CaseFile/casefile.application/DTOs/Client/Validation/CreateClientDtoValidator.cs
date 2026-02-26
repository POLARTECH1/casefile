using FluentValidation;

namespace casefile.application.DTOs.Client.Validation;

/// <summary>
/// Validateur du DTO de creation client.
/// </summary>
public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
{
    public CreateClientDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom du client est obligatoire.")
            .MaximumLength(150).WithMessage("Le nom du client ne peut pas dépasser 150 caractères.");
        RuleFor(x => x.Prenom)
            .NotEmpty().WithMessage("Le prénom du client est obligatoire.")
            .MaximumLength(150).WithMessage("Le prénom du client ne peut pas dépasser 150 caractères.");
        RuleFor(x => x.Telephone)
            .NotEmpty().WithMessage("Le téléphone du client est obligatoire.")
            .MaximumLength(50).WithMessage("Le téléphone du client ne peut pas dépasser 50 caractères.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'email du client est obligatoire.")
            .EmailAddress().WithMessage("L'email du client n'est pas valide.")
            .MaximumLength(320).WithMessage("L'email du client ne peut pas dépasser 320 caractères.");
    }
}
