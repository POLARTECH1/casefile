namespace casefile.domain.model;

/// <summary>
/// Élément d'un template de dossier (peut contenir des sous-éléments).
/// </summary>
public class TemplateDossierElement
{
    /// <summary>
    /// Identifiant unique de l'élément.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nom de l'élément.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Identifiant de l'élément parent, si présent.
    /// </summary>
    public Guid? IdParent { get; set; }

    /// <summary>
    /// Ordre d'affichage dans la hiérarchie.
    /// </summary>
    public int Ordre { get; set; }

    /// <summary>
    /// Parent de l'élément, si présent.
    /// </summary>
    public TemplateDossierElement? Parent { get; set; }

    /// <summary>
    /// Sous-éléments rattachés.
    /// </summary>
    public ICollection<TemplateDossierElement> Enfants { get; set; } = new List<TemplateDossierElement>();

    /// <summary>
    /// Documents attendus à ce niveau.
    /// </summary>
    public ICollection<DocumentAttendu> DocumentsAttendus { get; set; } = new List<DocumentAttendu>();
}
