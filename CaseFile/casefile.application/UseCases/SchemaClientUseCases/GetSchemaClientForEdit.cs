using casefile.application.DTOs.DefinitionAttribut;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.SchemasClients;
using FluentResults;

namespace casefile.application.UseCases.SchemaClientUseCases;

public class GetSchemaClientForEdit(ISchemaClientRepository schemaClientRepository) : IGetSchemaClientForEdit
{
    public async Task<Result<UpdateSchemaClientDto>> ExecuteAsync(Guid id)
    {
        var result = await schemaClientRepository.GetAllAsync(new SchemaClientByIdWithDefinitionsSpecification(id));
        if (result.IsFailed)
        {
            return Result.Fail<UpdateSchemaClientDto>(result.Errors);
        }

        var schema = result.Value.FirstOrDefault();
        if (schema is null)
        {
            return Result.Fail<UpdateSchemaClientDto>($"SchemaClient introuvable pour l'identifiant {id}.");
        }

        return Result.Ok(new UpdateSchemaClientDto
        {
            Id = schema.Id,
            Nom = schema.Nom,
            CreateDefinitionAttributDtos = schema.Definitions
                .Select(d => new CreateDefinitionAttributDto
                {
                    Cle = d.Cle,
                    Libelle = d.Libelle,
                    Type = d.Type,
                    EstRequis = d.EstRequis,
                    ValeurDefaut = d.ValeurDefaut
                }).ToList()
        });
    }
}
