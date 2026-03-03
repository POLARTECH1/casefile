using FluentValidation;

namespace casefile.application.DTOs.DossierClient.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de dossier client.
/// </summary>
public class UpdateDossierClientDtoValidator : AbstractValidator<UpdateDossierClientDto>
{
    public UpdateDossierClientDtoValidator()
    {
        Include(new CreateDossierClientDtoValidator());
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'identifiant du dossier client est obligatoire.");
    }
}
