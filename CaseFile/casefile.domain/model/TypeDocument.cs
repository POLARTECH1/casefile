namespace casefile.domain.model;

/// <summary>
/// Type de document géré par le système.
/// </summary>
public class TypeDocument
{
    /// <summary>
    /// Identifiant unique du type.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code fonctionnel du type.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom du type de document.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Extensions autorisées (format libre, ex: ".pdf;.docx").
    /// </summary>
    public string? ExtensionsPermises { get; set; }

    /// <summary>
    /// Dossier cible par défaut pour ce type.
    /// </summary>
    public string? DossierCibleParDefaut { get; set; }

    /// <summary>
    /// Règle de nommage associée, si définie.
    /// </summary>
    public RegleNommageDocument? RegleNommage { get; set; }
}
