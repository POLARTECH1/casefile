namespace casefile.domain.model;

/// <summary>
/// Définit un attribut dynamique applicable à un client.
/// </summary>
public class DefinitionAttribut
{
    /// <summary>
    /// Identifiant unique de la définition.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Clé technique de l'attribut.
    /// </summary>
    public string Cle { get; set; } = string.Empty;

    /// <summary>
    /// Libellé affiché de l'attribut.
    /// </summary>
    public string Libelle { get; set; } = string.Empty;

    /// <summary>
    /// Type de valeur attendu.
    /// </summary>
    public TypeAttribut Type { get; set; }

    /// <summary>
    /// Indique si l'attribut est obligatoire.
    /// </summary>
    public bool EstRequis { get; set; }

    /// <summary>
    /// Valeur par défaut éventuelle.
    /// </summary>
    public string? ValeurDefaut { get; set; }
}
