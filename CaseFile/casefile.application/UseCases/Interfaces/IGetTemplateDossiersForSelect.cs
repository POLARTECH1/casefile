using casefile.application.DTOs.TemplateDossier;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

/// <summary>
/// Récupère une page de templates de dossier filtrés par nom, pour les dropdowns de sélection.
/// </summary>
public interface IGetTemplateDossiersForSelect
{
    Task<Result<(IEnumerable<TemplateDossierForSelectDto> Items, int TotalCount)>> ExecuteAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize);
}
