namespace casefile.application.DTOs.DefinitionAttribut;

/// <summary>
/// DTO utilise pour mettre a jour une definition d'attribut.
/// </summary>
public class UpdateDefinitionAttributDto : CreateDefinitionAttributDto
{
    public Guid Id { get; set; }
}
