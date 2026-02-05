namespace casefile.domain.model;

/// <summary>
/// Modèle de dossier virtuel utilisé pour organiser les documents.
/// </summary>
public class TemplateDossier
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
    /// Description optionnelle.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Éléments hiérarchiques du template.
    /// </summary>
    public ICollection<TemplateDossierElement> Elements { get; set; } = new List<TemplateDossierElement>();
}
