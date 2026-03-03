namespace casefile.application.DTOs.SchemaClient;

/// <summary>
/// DTO léger d'un schéma client avec ses définitions pour les dropdowns de sélection.
/// </summary>
public class SchemaClientForSelectDto
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public List<DefinitionAttributForSelectDto> Definitions { get; set; } = new();
}
