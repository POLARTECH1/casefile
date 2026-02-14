namespace casefile.application.DTOs.TypeDocument;

/// <summary>
/// DTO utilise pour mettre a jour un type de document.
/// </summary>
public class UpdateTypeDocumentDto : CreateTypeDocumentDto
{
    public Guid Id { get; set; }
}
