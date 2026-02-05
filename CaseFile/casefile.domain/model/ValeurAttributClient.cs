namespace casefile.domain.model;

/// <summary>
/// Valeur concrète d'un attribut pour un client.
/// </summary>
public class ValeurAttributClient
{
    /// <summary>
    /// Identifiant unique de la valeur.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Clé de l'attribut associé.
    /// </summary>
    public string Cle { get; set; } = string.Empty;

    /// <summary>
    /// Valeur stockée sous forme texte.
    /// </summary>
    public string Valeur { get; set; } = string.Empty;
}
