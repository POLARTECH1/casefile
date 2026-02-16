using FluentValidation;

namespace casefile.application.DTOs.ValeurAttributClient.Validation;

/// <summary>
/// Validateur du DTO de creation de valeur d'attribut client.
/// </summary>
public class CreateValeurAttributClientDtoValidator : AbstractValidator<CreateValeurAttributClientDto>
{
    public CreateValeurAttributClientDtoValidator()
    {
        RuleFor(x => x.Cle).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Valeur).NotEmpty().MaximumLength(4000);
    }
}
