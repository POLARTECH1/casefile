namespace casefile.application.DTOs.TemplateDossier;

/// <summary>
/// DTO léger d'un template de dossier pour les dropdowns de sélection.
/// </summary>
public class TemplateDossierForSelectDto
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
}
