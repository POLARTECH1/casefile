namespace casefile.application.DTOs.SchemaClient;

/// <summary>
/// DTO de resume pour l'affichage d'un schema client dans la liste.
/// </summary>
public class SchemaClientItemDto
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public int NombreDeProprietes { get; set; }
    public int NombreDeClientsQuiUtilisentCeSchema { get; set; }
}
