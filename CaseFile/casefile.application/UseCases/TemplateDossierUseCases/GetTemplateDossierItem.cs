using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.TemplateDossiers;
using FluentResults;

namespace casefile.application.UseCases.TemplateDossierUseCases;

/// <summary>
/// Recupere un template de dossier et ses metriques pour l'affichage.
/// </summary>
public class GetTemplateDossierItem(
    ITemplateDossierRepository templateDossierRepository,
    IClientRepository clientRepository) : IGetTemplateDossierItem
{
    public async Task<Result<TemplateDossierItemDto>> ExecuteAsync(Guid id)
    {
        var templatesResult = await templateDossierRepository.GetAllAsync(
            new TemplateDossierByIdWithElementsAndDocumentsSpecification(id));
        if (templatesResult.IsFailed)
        {
            return Result.Fail<TemplateDossierItemDto>(templatesResult.Errors);
        }

        var template = templatesResult.Value.FirstOrDefault();
        if (template is null)
        {
            return Result.Fail<TemplateDossierItemDto>($"TemplateDossier introuvable pour l'identifiant {id}.");
        }

        var clientsResult = await clientRepository.GetAllAsync();
        if (clientsResult.IsFailed)
        {
            return Result.Fail<TemplateDossierItemDto>(clientsResult.Errors);
        }

        var nombreClientsQuiUtilisentCeTemplate = clientsResult.Value.Count(c => c.TemplateDossier?.Id == id);

        var item = new TemplateDossierItemDto
        {
            Id = template.Id,
            Nom = template.Nom,
            Description = template.Description,
            NombreDeDossiers = template.Elements.Count,
            NombreDocumentsAttendus = template.Elements.Sum(e => e.DocumentsAttendus.Count),
            NombreDeClientsQuiUtilisentCeTemplate = nombreClientsQuiUtilisentCeTemplate
        };

        return Result.Ok(item);
    }
}
