using FluentValidation;

namespace casefile.application.DTOs.Client.Validation;

/// <summary>
/// Validateur du DTO de creation client.
/// </summary>
public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
{
    public CreateClientDtoValidator()
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Prenom).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Telephone).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(320);
    }
}
