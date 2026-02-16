namespace casefile.application.DTOs.TemplateDossierElement;

/// <summary>
/// DTO utilise pour mettre a jour un element de template dossier.
/// </summary>
public class UpdateTemplateDossierElementDto : CreateTemplateDossierElementDto
{
    public Guid Id { get; set; }
}
