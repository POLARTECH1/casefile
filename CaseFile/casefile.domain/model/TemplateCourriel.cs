namespace casefile.domain.model;

/// <summary>
/// Modèle de courriel pour générer des messages.
/// </summary>
public class TemplateCourriel
{
    /// <summary>
    /// Identifiant unique du template.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nom du template.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Sujet du courriel.
    /// </summary>
    public string Sujet { get; set; } = string.Empty;

    /// <summary>
    /// Corps du courriel.
    /// </summary>
    public string Corps { get; set; } = string.Empty;

    /// <summary>
    /// Template de dossier lié, si applicable.
    /// </summary>
    public TemplateDossier? TemplateDossier { get; set; }
}
