namespace casefile.application.DTOs.TemplateCourriel;

/// <summary>
/// DTO utilise pour mettre a jour un template courriel.
/// </summary>
public class UpdateTemplateCourrielDto : CreateTemplateCourrielDto
{
    public Guid Id { get; set; }
}
