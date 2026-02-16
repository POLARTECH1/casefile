using FluentValidation;

namespace casefile.application.DTOs.CourrielEnvoye.Validation;

/// <summary>
/// Validateur du DTO de creation de courriel envoye.
/// </summary>
public class CreateCourrielEnvoyeDtoValidator : AbstractValidator<CreateCourrielEnvoyeDto>
{
    public CreateCourrielEnvoyeDtoValidator()
    {
        RuleFor(x => x.A).NotEmpty().EmailAddress().MaximumLength(320);
        RuleFor(x => x.Sujet).NotEmpty().MaximumLength(200);
        RuleFor(x => x.PiecesJointes).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.EnvoyeLe).LessThanOrEqualTo(DateTime.UtcNow.AddDays(1));
    }
}
