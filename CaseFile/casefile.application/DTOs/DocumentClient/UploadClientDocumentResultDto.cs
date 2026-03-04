namespace casefile.application.DTOs.DocumentClient;

public class UploadClientDocumentResultDto
{
    public Guid DocumentClientId { get; set; }
    public string CheminPhysique { get; set; } = string.Empty;
    public string NomOriginal { get; set; } = string.Empty;
    public string NomDossier { get; set; } = string.Empty;
}
