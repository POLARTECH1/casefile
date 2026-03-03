namespace casefile.application.DTOs.Client;

public class ShowClientDto
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NomSchema { get; set; } = string.Empty;
    public int NombreDocuments { get; set; }
}
