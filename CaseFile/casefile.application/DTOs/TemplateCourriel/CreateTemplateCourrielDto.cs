namespace casefile.application.DTOs.TemplateCourriel;

/// <summary>
/// DTO utilise pour creer un template courriel.
/// </summary>
public class CreateTemplateCourrielDto
{
    public string Nom { get; set; } = string.Empty;
    public string Sujet { get; set; } = string.Empty;
    public string Corps { get; set; } = string.Empty;
}
