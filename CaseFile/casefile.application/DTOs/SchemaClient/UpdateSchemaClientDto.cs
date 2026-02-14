namespace casefile.application.DTOs.SchemaClient;

/// <summary>
/// DTO utilise pour mettre a jour un schema client.
/// </summary>
public class UpdateSchemaClientDto : CreateSchemaClientDto
{
    public Guid Id { get; set; }
}
