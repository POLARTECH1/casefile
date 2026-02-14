using FluentValidation;

namespace casefile.application.DTOs.TemplateCourriel.Validation;

/// <summary>
/// Validateur du DTO de creation de template courriel.
/// </summary>
public class CreateTemplateCourrielDtoValidator : AbstractValidator<CreateTemplateCourrielDto>
{
    public CreateTemplateCourrielDtoValidator()
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Sujet).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Corps).NotEmpty().MaximumLength(20000);
    }
}
