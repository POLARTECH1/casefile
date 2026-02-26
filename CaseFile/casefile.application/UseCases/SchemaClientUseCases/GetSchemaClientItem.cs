using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.Clients;
using casefile.data.Specifications.SchemasClients;
using FluentResults;

namespace casefile.application.UseCases.SchemaClientUseCases;

public class GetSchemaClientItem(
    ISchemaClientRepository schemaClientRepository,
    IClientRepository clientRepository) : IGetSchemaClientItem
{
    public async Task<Result<SchemaClientItemDto>> ExecuteAsync(Guid id)
    {
        var schemasResult = await schemaClientRepository.GetAllAsync(
            new SchemaClientByIdWithDefinitionsSpecification(id));
        if (schemasResult.IsFailed)
        {
            return Result.Fail<SchemaClientItemDto>(schemasResult.Errors);
        }

        var schema = schemasResult.Value.FirstOrDefault();
        if (schema is null)
        {
            return Result.Fail<SchemaClientItemDto>($"SchemaClient introuvable pour l'identifiant {id}.");
        }

        var countResult = await clientRepository.CountAsync(new ClientBySchemaClientIdSpecification(id));
        if (countResult.IsFailed)
        {
            return Result.Fail<SchemaClientItemDto>(countResult.Errors);
        }

        return Result.Ok(new SchemaClientItemDto
        {
            Id = schema.Id,
            Nom = schema.Nom,
            NombreDeProprietes = schema.Definitions.Count,
            NombreDeClientsQuiUtilisentCeSchema = countResult.Value
        });
    }
}
