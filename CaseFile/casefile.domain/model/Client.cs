namespace casefile.domain.model;

/// <summary>
/// Représente un client et ses informations principales.
/// </summary>
public class Client
{
    /// <summary>
    /// Identifiant unique du client.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nom de famille du client.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Prénom du client.
    /// </summary>
    public string Prenom { get; set; } = string.Empty;

    /// <summary>
    /// Numéro de téléphone principal.
    /// </summary>
    public string Telephone { get; set; } = string.Empty;

    /// <summary>
    /// Adresse courriel du client.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Date de création du client.
    /// </summary>
    public DateTime CreeLe { get; set; }

    /// <summary>
    /// Schéma d'attributs utilisé pour ce client.
    /// </summary>
    public SchemaClient? SchemaClient { get; set; }

    /// <summary>
    /// Valeurs d'attributs renseignées pour ce client.
    /// </summary>
    public ICollection<ValeurAttributClient> ValeursAttributs { get; set; } = new List<ValeurAttributClient>();

    /// <summary>
    /// Dossiers virtuels rattachés au client.
    /// </summary>
    public ICollection<DossierClient> Dossiers { get; set; } = new List<DossierClient>();

    /// <summary>
    /// Modèle de dossier associé au client.
    /// </summary>
    public TemplateDossier? TemplateDossier { get; set; }
}
