namespace casefile.domain.model;

/// <summary>
/// Dossier virtuel associé à un client.
/// </summary>
public class DossierClient
{
    /// <summary>
    /// Identifiant unique du dossier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nom du dossier.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Chemin virtuel du dossier.
    /// </summary>
    public string CheminVirtuel { get; set; } = string.Empty;

    /// <summary>
    /// Documents rattachés au dossier.
    /// </summary>
    public ICollection<DocumentClient> Documents { get; set; } = new List<DocumentClient>();

    /// <summary>
    /// Courriels envoyés liés à ce dossier.
    /// </summary>
    public ICollection<CourrielEnvoye> Courriels { get; set; } = new List<CourrielEnvoye>();
}
