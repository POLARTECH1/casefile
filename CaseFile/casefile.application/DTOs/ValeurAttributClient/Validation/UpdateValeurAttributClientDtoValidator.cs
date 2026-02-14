using FluentValidation;

namespace casefile.application.DTOs.ValeurAttributClient.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de valeur d'attribut client.
/// </summary>
public class UpdateValeurAttributClientDtoValidator : AbstractValidator<UpdateValeurAttributClientDto>
{
    public UpdateValeurAttributClientDtoValidator()
    {
        Include(new CreateValeurAttributClientDtoValidator());
        RuleFor(x => x.Id).NotEmpty();
    }
}
