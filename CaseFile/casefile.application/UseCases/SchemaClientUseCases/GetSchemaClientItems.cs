using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.Clients;
using casefile.data.Specifications.SchemasClients;
using FluentResults;

namespace casefile.application.UseCases.SchemaClientUseCases;

public class GetSchemaClientItems(
    ISchemaClientRepository schemaClientRepository,
    IClientRepository clientRepository) : IGetSchemaClientItems
{
    public async Task<Result<IEnumerable<SchemaClientItemDto>>> ExecuteAsync()
    {
        var schemasResult = await schemaClientRepository.GetAllAsync(new SchemaClientWithDefinitionsSpecification());
        if (schemasResult.IsFailed)
        {
            return Result.Fail<IEnumerable<SchemaClientItemDto>>(schemasResult.Errors);
        }

        var items = new List<SchemaClientItemDto>();
        foreach (var schema in schemasResult.Value)
        {
            var countResult = await clientRepository.CountAsync(new ClientBySchemaClientIdSpecification(schema.Id));
            if (countResult.IsFailed)
            {
                return Result.Fail<IEnumerable<SchemaClientItemDto>>(countResult.Errors);
            }

            items.Add(new SchemaClientItemDto
            {
                Id = schema.Id,
                Nom = schema.Nom,
                NombreDeProprietes = schema.Definitions.Count,
                NombreDeClientsQuiUtilisentCeSchema = countResult.Value
            });
        }

        return Result.Ok<IEnumerable<SchemaClientItemDto>>(items.OrderBy(s => s.Nom));
    }
}
