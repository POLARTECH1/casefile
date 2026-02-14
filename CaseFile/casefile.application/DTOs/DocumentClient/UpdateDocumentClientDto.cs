namespace casefile.application.DTOs.DocumentClient;

/// <summary>
/// DTO utilise pour mettre a jour un document client.
/// </summary>
public class UpdateDocumentClientDto : CreateDocumentClientDto
{
    public Guid Id { get; set; }
}
