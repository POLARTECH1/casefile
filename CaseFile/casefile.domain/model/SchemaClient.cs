namespace casefile.domain.model;

/// <summary>
/// Décrit un schéma d'attributs personnalisés pour les clients.
/// </summary>
public class SchemaClient
{
    /// <summary>
    /// Identifiant unique du schéma.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nom du schéma.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Définitions des attributs appartenant au schéma.
    /// </summary>
    public ICollection<DefinitionAttribut> Definitions { get; set; } = new List<DefinitionAttribut>();
}
