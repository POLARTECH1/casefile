using FluentValidation;

namespace casefile.application.DTOs.TemplateCourriel.Validation;

/// <summary>
/// Validateur du DTO de mise a jour de template courriel.
/// </summary>
public class UpdateTemplateCourrielDtoValidator : AbstractValidator<UpdateTemplateCourrielDto>
{
    public UpdateTemplateCourrielDtoValidator()
    {
        Include(new CreateTemplateCourrielDtoValidator());
        RuleFor(x => x.Id).NotEmpty();
    }
}
