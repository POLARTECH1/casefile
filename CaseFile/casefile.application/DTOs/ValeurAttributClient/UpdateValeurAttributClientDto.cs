namespace casefile.application.DTOs.ValeurAttributClient;

/// <summary>
/// DTO utilise pour mettre a jour une valeur d'attribut client.
/// </summary>
public class UpdateValeurAttributClientDto : CreateValeurAttributClientDto
{
    public Guid Id { get; set; }
}
