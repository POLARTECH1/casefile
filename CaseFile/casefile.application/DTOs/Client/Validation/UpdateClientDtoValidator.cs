using FluentValidation;

namespace casefile.application.DTOs.Client.Validation;

/// <summary>
/// Validateur du DTO de mise a jour client.
/// </summary>
public class UpdateClientDtoValidator : AbstractValidator<UpdateClientDto>
{
    public UpdateClientDtoValidator()
    {
        Include(new CreateClientDtoValidator());
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("L'identifiant du client est obligatoire.");
    }
}
