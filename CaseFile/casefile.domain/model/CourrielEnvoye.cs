
namespace casefile.domain.model;

/// <summary>
/// Courriel déjà envoyé à un client.
/// </summary>
public class CourrielEnvoye
{
    /// <summary>
    /// Identifiant unique du courriel.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Destinataire du courriel.
    /// </summary>
    public string A { get; set; } = string.Empty;

    /// <summary>
    /// Sujet du courriel.
    /// </summary>
    public string Sujet { get; set; } = string.Empty;

    /// <summary>
    /// Date d'envoi du courriel.
    /// </summary>
    public DateTime EnvoyeLe { get; set; }

    /// <summary>
    /// Pièces jointes (format libre).
    /// </summary>
    public string PiecesJointes { get; set; } = string.Empty;
}