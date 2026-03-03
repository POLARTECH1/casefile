using casefile.application.DTOs.ValeurAttributClient;

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

    /// <summary>
    /// Identifiant du schéma d'attributs à associer au client.
    /// </summary>
    public Guid? SchemaClientId { get; set; }

    /// <summary>
    /// Identifiant du template de dossier à associer au client.
    /// </summary>
    public Guid? TemplateDossierId { get; set; }

    /// <summary>
    /// Valeurs des attributs dynamiques du schéma choisi.
    /// </summary>
    public List<CreateValeurAttributClientDto> ValeursAttributs { get; set; } = new();
}
