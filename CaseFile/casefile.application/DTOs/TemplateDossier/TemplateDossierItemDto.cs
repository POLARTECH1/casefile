namespace casefile.application.DTOs.TemplateDossier;

/// <summary>
/// DTO de résumé pour l'affichage d'un template de dossier dans la liste.
/// </summary>
public class TemplateDossierItemDto
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int NombreDeDossiers { get; set; }
    public int NombreDocumentsAttendus { get; set; }
    public int NombreDeClientsQuiUtilisentCeTemplate { get; set; }
}
