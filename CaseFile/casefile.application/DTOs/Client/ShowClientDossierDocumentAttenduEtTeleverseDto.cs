namespace casefile.application.DTOs.Client;

public class ShowClientDossierDocumentAttenduEtTeleverseDto
{
    public Guid Id { get; set; }
    public string NomTypeDocumentAttendu { get; set; } = string.Empty;
    public bool EstRequis { get; set; }
    public bool IsIncomplet { get; set; }
    public bool IsDocumentAttenduPresentDansDossierClient { get; set; }
    public string ExtensionTypeDocumentAttendu { get; set; } = string.Empty;
    public Guid? DocumentClientId { get; set; }
    public string DocumentClientNomOriginal { get; set; } = string.Empty;
    public string DocumentClientNomStandardise { get; set; } = string.Empty;
    public string DocumentClientCheminPhysique { get; set; } = string.Empty;
    public string DocumentClientExtension { get; set; } = string.Empty;
    public DateTime? DocumentClientAjouteLe { get; set; }
}
