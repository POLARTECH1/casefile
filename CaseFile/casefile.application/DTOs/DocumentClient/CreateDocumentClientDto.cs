namespace casefile.application.DTOs.DocumentClient;

/// <summary>
/// DTO utilise pour creer un document client.
/// </summary>
public class CreateDocumentClientDto
{
    public string NomOriginal { get; set; } = string.Empty;
    public string NomStandardise { get; set; } = string.Empty;
    public string CheminPhysique { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public DateTime AjouteLe { get; set; }
}
