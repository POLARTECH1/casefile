using casefile.application.DTOs.SchemaClient;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

/// <summary>
/// Récupère une page de schémas clients filtrés par nom, pour les dropdowns de sélection.
/// </summary>
public interface IGetSchemaClientsForSelect
{
    Task<Result<(IEnumerable<SchemaClientForSelectDto> Items, int TotalCount)>> ExecuteAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize);
}
