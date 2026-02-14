namespace casefile.application.DTOs.ProfilEntreprise;

/// <summary>
/// DTO utilise pour creer un profil entreprise.
/// </summary>
public class CreateProfilEntrepriseDto
{
    public string Nom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telephone { get; set; }
    public string? Adresse { get; set; }
    public string? LogoPath { get; set; }
    public string? Signature { get; set; }
}
