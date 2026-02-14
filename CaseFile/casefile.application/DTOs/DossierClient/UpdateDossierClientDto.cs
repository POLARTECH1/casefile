namespace casefile.application.DTOs.DossierClient;

/// <summary>
/// DTO utilise pour mettre a jour un dossier client.
/// </summary>
public class UpdateDossierClientDto : CreateDossierClientDto
{
    public Guid Id { get; set; }
}
