namespace casefile.application.DTOs.DocumentAttendu;

/// <summary>
/// DTO utilise pour mettre a jour un document attendu.
/// </summary>
public class UpdateDocumentAttenduDto : CreateDocumentAttenduDto
{
    public Guid Id { get; set; }
}
