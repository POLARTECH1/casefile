namespace casefile.application.DTOs.Client;

/// <summary>
/// DTO de resume pour l'affichage d'un client dans la liste.
/// </summary>
public class ClientItemDto
{
    public Guid Id { get; set; }
    public string NomPrenom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NomSchema { get; set; } = string.Empty;
    public int NombreDocuments { get; set; }
}
