namespace casefile.application.DTOs.Client;

/// <summary>
/// DTO utilise pour creer un client.
/// </summary>
public class CreateClientDto
{
    /// <summary>
    /// Nom de famille du client.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Prenom du client.
    /// </summary>
    public string Prenom { get; set; } = string.Empty;

    /// <summary>
    /// Numero de telephone principal.
    /// </summary>
    public string Telephone { get; set; } = string.Empty;

    /// <summary>
    /// Adresse courriel du client.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
