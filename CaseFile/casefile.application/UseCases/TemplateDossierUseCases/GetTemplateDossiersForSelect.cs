using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.TemplateDossiers;
using FluentResults;

namespace casefile.application.UseCases.TemplateDossierUseCases;

/// <summary>
/// Récupère une page de templates de dossier filtrés par nom,
/// pour alimenter les dropdowns de sélection paginés.
/// </summary>
public class GetTemplateDossiersForSelect(ITemplateDossierRepository templateDossierRepository)
    : IGetTemplateDossiersForSelect
{
    public async Task<Result<(IEnumerable<TemplateDossierForSelectDto> Items, int TotalCount)>> ExecuteAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        var spec = new TemplateDossierPaginatedSearchSpecification(searchTerm, pageNumber, pageSize);

        var templatesResult = await templateDossierRepository.GetAllAsync(spec);
        if (templatesResult.IsFailed)
            return Result.Fail<(IEnumerable<TemplateDossierForSelectDto>, int)>(templatesResult.Errors);

        var countResult = await templateDossierRepository.CountAsync(spec);
        if (countResult.IsFailed)
            return Result.Fail<(IEnumerable<TemplateDossierForSelectDto>, int)>(countResult.Errors);

        var items = templatesResult.Value.Select(t => new TemplateDossierForSelectDto
        {
            Id = t.Id,
            Nom = t.Nom
        }).ToList();

        return Result.Ok(((IEnumerable<TemplateDossierForSelectDto>)items, countResult.Value));
    }
}
