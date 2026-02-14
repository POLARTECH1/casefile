namespace casefile.application.DTOs.Client;

/// <summary>
/// DTO utilise pour mettre a jour un client.
/// </summary>
public class UpdateClientDto : CreateClientDto
{
    /// <summary>
    /// Identifiant du client.
    /// </summary>
    public Guid Id { get; set; }
}
