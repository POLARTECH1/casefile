using casefile.application.DTOs.DefinitionAttribut;

namespace casefile.application.DTOs.SchemaClient;

/// <summary>
/// DTO utilise pour creer un schema client.
/// </summary>
public class CreateSchemaClientDto
{
    public string Nom { get; set; } = string.Empty;
    public List<CreateDefinitionAttributDto> CreateDefinitionAttributDtos { get; set; } = new();
}
