using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.TemplateDossiers;
using FluentResults;

namespace casefile.application.UseCases.TemplateDossierUseCases;

/// <summary>
/// Récupère la liste des templates de dossier et leurs métriques pour l'affichage.
/// </summary>
public class GetTemplateDossierItems(
    ITemplateDossierRepository templateDossierRepository,
    IClientRepository clientRepository) : IGetTemplateDossierItems
{
    public async Task<Result<IEnumerable<TemplateDossierItemDto>>> ExecuteAsync()
    {
        var templatesResult =
            await templateDossierRepository.GetAllAsync(new TemplateDossierWithElementsAndDocumentsSpecification());
        if (templatesResult.IsFailed)
        {
            return Result.Fail<IEnumerable<TemplateDossierItemDto>>(templatesResult.Errors);
        }

        var clientsResult = await clientRepository.GetAllAsync();
        if (clientsResult.IsFailed)
        {
            return Result.Fail<IEnumerable<TemplateDossierItemDto>>(clientsResult.Errors);
        }

        var nombreClientsParTemplate = clientsResult.Value
            .Where(c => c.TemplateDossier is not null)
            .GroupBy(c => c.TemplateDossier!.Id)
            .ToDictionary(g => g.Key, g => g.Count());

        var items = templatesResult.Value
            .Select(template => new TemplateDossierItemDto
            {
                Id = template.Id,
                Nom = template.Nom,
                Description = template.Description,
                NombreDeDossiers = template.Elements.Count,
                NombreDocumentsAttendus = template.Elements.Sum(e => e.DocumentsAttendus.Count),
                NombreDeClientsQuiUtilisentCeTemplate =
                    nombreClientsParTemplate.TryGetValue(template.Id, out var count) ? count : 0
            })
            .OrderBy(t => t.Nom);

        return Result.Ok<IEnumerable<TemplateDossierItemDto>>(items);
    }
}
