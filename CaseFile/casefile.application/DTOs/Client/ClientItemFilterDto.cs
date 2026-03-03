namespace casefile.application.DTOs.Client;

/// <summary>
/// Filtres disponibles pour la recherche de clients dans la liste.
/// </summary>
public class ClientItemFilterDto
{
    public string? NomPrenom { get; set; }
    public string? Email { get; set; }
    public string? NomSchema { get; set; }
    public int? NombreDocuments { get; set; }
}
