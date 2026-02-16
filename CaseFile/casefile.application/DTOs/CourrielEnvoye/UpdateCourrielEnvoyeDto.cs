namespace casefile.application.DTOs.CourrielEnvoye;

/// <summary>
/// DTO utilise pour mettre a jour un courriel envoye.
/// </summary>
public class UpdateCourrielEnvoyeDto : CreateCourrielEnvoyeDto
{
    public Guid Id { get; set; }
}
