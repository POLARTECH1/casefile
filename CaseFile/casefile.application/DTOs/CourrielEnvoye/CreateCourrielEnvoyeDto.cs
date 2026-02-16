namespace casefile.application.DTOs.CourrielEnvoye;

/// <summary>
/// DTO utilise pour creer un courriel envoye.
/// </summary>
public class CreateCourrielEnvoyeDto
{
    public string A { get; set; } = string.Empty;
    public string Sujet { get; set; } = string.Empty;
    public DateTime EnvoyeLe { get; set; }
    public string PiecesJointes { get; set; } = string.Empty;
}
