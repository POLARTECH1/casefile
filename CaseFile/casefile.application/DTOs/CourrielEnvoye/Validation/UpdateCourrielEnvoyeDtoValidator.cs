using FluentValidation;

namespace casefile.application.DTOs.CourrielEnvoye.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de courriel envoye.
/// </summary>
public class UpdateCourrielEnvoyeDtoValidator : AbstractValidator<UpdateCourrielEnvoyeDto>
{
    public UpdateCourrielEnvoyeDtoValidator()
    {
        Include(new CreateCourrielEnvoyeDtoValidator());
        RuleFor(x => x.Id).NotEmpty();
    }
}
