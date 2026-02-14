namespace casefile.application.DTOs.TemplateDossier;

/// <summary>
/// DTO utilise pour mettre a jour un template dossier.
/// </summary>
public class UpdateTemplateDossierDto : CreateTemplateDossierDto
{
    public Guid Id { get; set; }
}
