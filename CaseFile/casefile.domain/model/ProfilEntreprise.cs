namespace casefile.domain.model;

/// <summary>
/// Profil d'entreprise utilisé pour l'envoi de courriels.
/// </summary>
public class ProfilEntreprise
{
    /// <summary>
    /// Identifiant unique du profil.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nom de l'entreprise.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Adresse courriel principale.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Numéro de téléphone, si applicable.
    /// </summary>
    public string? Telephone { get; set; }

    /// <summary>
    /// Adresse postale, si applicable.
    /// </summary>
    public string? Adresse { get; set; }

    /// <summary>
    /// Chemin vers le logo de l'entreprise.
    /// </summary>
    public string? LogoPath { get; set; }

    /// <summary>
    /// Signature utilisée dans les courriels.
    /// </summary>
    public string? Signature { get; set; }

    /// <summary>
    /// Templates de courriel disponibles pour ce profil.
    /// </summary>
    public ICollection<TemplateCourriel> TemplatesCourriel { get; set; } = new List<TemplateCourriel>();
}
