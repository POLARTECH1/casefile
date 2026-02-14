namespace casefile.application.DTOs.ProfilEntreprise;

/// <summary>
/// DTO utilise pour mettre a jour un profil entreprise.
/// </summary>
public class UpdateProfilEntrepriseDto : CreateProfilEntrepriseDto
{
    public Guid Id { get; set; }
}
