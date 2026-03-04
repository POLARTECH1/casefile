namespace casefile.application.DTOs.Client;

public class ShowClientDossierDto
{
    public string Nom { get; set; } = string.Empty;
    public int NombreDocuments { get; set; }
    public int NombreDocumentRequis { get; set; }
    public bool IsDossierComplet { get; set; }
    public List<ShowClientDossierDocumentAttenduEtTeleverseDto> DocumentsAttendusEtTeleverses { get; set; } = new();
}
