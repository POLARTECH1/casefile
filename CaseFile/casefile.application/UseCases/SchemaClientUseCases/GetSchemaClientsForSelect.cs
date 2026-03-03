using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.SchemasClients;
using FluentResults;

namespace casefile.application.UseCases.SchemaClientUseCases;

/// <summary>
/// Récupère une page de schémas clients avec leurs définitions, filtrés par nom,
/// pour alimenter les dropdowns de sélection paginés.
/// </summary>
public class GetSchemaClientsForSelect(ISchemaClientRepository schemaClientRepository) : IGetSchemaClientsForSelect
{
    public async Task<Result<(IEnumerable<SchemaClientForSelectDto> Items, int TotalCount)>> ExecuteAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        var spec = new SchemaClientPaginatedSearchSpecification(searchTerm, pageNumber, pageSize);

        var schemasResult = await schemaClientRepository.GetAllAsync(spec);
        if (schemasResult.IsFailed)
            return Result.Fail<(IEnumerable<SchemaClientForSelectDto>, int)>(schemasResult.Errors);

        var countResult = await schemaClientRepository.CountAsync(spec);
        if (countResult.IsFailed)
            return Result.Fail<(IEnumerable<SchemaClientForSelectDto>, int)>(countResult.Errors);

        var items = schemasResult.Value.Select(s => new SchemaClientForSelectDto
        {
            Id = s.Id,
            Nom = s.Nom,
            Definitions = s.Definitions.Select(d => new DefinitionAttributForSelectDto
            {
                Id = d.Id,
                Cle = d.Cle,
                Libelle = d.Libelle,
                Type = d.Type,
                EstRequis = d.EstRequis,
                ValeurDefaut = d.ValeurDefaut
            }).ToList()
        }).ToList();

        return Result.Ok(((IEnumerable<SchemaClientForSelectDto>)items, countResult.Value));
    }
}
