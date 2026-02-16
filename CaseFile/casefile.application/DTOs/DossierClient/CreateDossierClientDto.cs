namespace casefile.application.DTOs.DossierClient;

/// <summary>
/// DTO utilise pour creer un dossier client.
/// </summary>
public class CreateDossierClientDto
{
    public string Nom { get; set; } = string.Empty;
    public string CheminVirtuel { get; set; } = string.Empty;
}
