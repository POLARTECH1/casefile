using FluentValidation;

namespace casefile.application.DTOs.DossierClient.Validation;

/// <summary>
/// Validateur du DTO de creation de dossier client.
/// </summary>
public class CreateDossierClientDtoValidator : AbstractValidator<CreateDossierClientDto>
{
    public CreateDossierClientDtoValidator()
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(150);
        RuleFor(x => x.CheminVirtuel).NotEmpty().MaximumLength(500);
    }
}
