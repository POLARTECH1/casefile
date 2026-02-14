namespace casefile.application.DTOs.RegleNommageDocument;

/// <summary>
/// DTO utilise pour mettre a jour une regle de nommage de document.
/// </summary>
public class UpdateRegleNommageDocumentDto : CreateRegleNommageDocumentDto
{
    public Guid Id { get; set; }
}
