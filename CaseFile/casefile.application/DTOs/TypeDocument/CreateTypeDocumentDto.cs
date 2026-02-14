namespace casefile.application.DTOs.TypeDocument;

/// <summary>
/// DTO utilise pour creer un type de document.
/// </summary>
public class CreateTypeDocumentDto
{
    public string Code { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string? ExtensionsPermises { get; set; }
    public string? DossierCibleParDefaut { get; set; }
}
