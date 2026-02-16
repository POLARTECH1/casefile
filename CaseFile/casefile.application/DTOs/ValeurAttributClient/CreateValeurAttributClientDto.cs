namespace casefile.application.DTOs.ValeurAttributClient;

/// <summary>
/// DTO utilise pour creer une valeur d'attribut client.
/// </summary>
public class CreateValeurAttributClientDto
{
    public string Cle { get; set; } = string.Empty;
    public string Valeur { get; set; } = string.Empty;
}
