namespace casefile.application.DTOs.Client;

/// <summary>
/// DTO de lecture d'un client.
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Identifiant du client.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nom de famille.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Prenom.
    /// </summary>
    public string Prenom { get; set; } = string.Empty;

    /// <summary>
    /// Telephone.
    /// </summary>
    public string Telephone { get; set; } = string.Empty;

    /// <summary>
    /// Courriel.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Date de creation.
    /// </summary>
    public DateTime CreeLe { get; set; }
}
