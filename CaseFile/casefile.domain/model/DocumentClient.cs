namespace casefile.domain.model;

/// <summary>
/// Document réel fourni par un client.
/// </summary>
public class DocumentClient
{
    /// <summary>
    /// Identifiant unique du document.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nom du fichier d'origine.
    /// </summary>
    public string NomOriginal { get; set; } = string.Empty;

    /// <summary>
    /// Nom standardisé utilisé par le système.
    /// </summary>
    public string NomStandardise { get; set; } = string.Empty;

    /// <summary>
    /// Chemin physique du fichier.
    /// </summary>
    public string CheminPhysique { get; set; } = string.Empty;

    /// <summary>
    /// Extension du fichier.
    /// </summary>
    public string Extension { get; set; } = string.Empty;

    /// <summary>
    /// Date d'ajout du document.
    /// </summary>
    public DateTime AjouteLe { get; set; }

    /// <summary>
    /// Type de document, si connu.
    /// </summary>
    public TypeDocument? TypeDocument { get; set; }
}
