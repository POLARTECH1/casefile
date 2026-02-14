using FluentValidation;

namespace casefile.application.DTOs.DefinitionAttribut.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de definition d'attribut.
/// </summary>
public class UpdateDefinitionAttributDtoValidator : AbstractValidator<UpdateDefinitionAttributDto>
{
    public UpdateDefinitionAttributDtoValidator()
    {
        Include(new CreateDefinitionAttributDtoValidator());
        RuleFor(x => x.Id).NotEmpty();
    }
}
