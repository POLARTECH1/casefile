namespace casefile.domain.model;

/// <summary>
/// Document attendu dans un élément de template.
/// </summary>
public class DocumentAttendu
{
    /// <summary>
    /// Identifiant unique du document attendu.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifiant du type de document attendu, si défini.
    /// </summary>
    public Guid? IdTypeDocument { get; set; }

    /// <summary>
    /// Type de document attendu.
    /// </summary>
    public TypeDocument? TypeDocument { get; set; }
}
