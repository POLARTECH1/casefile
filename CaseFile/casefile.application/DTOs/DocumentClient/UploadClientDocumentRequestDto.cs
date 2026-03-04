namespace casefile.application.DTOs.DocumentClient;

public class UploadClientDocumentRequestDto
{
    public Guid ClientId { get; set; }
    public string NomDossier { get; set; } = string.Empty;
    public string CheminFichierSource { get; set; } = string.Empty;
    public Guid? TypeDocumentId { get; set; }
}
