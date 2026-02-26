using AutoMapper;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.SchemasClients;
using casefile.domain.model;
using FluentResults;
using FluentValidation;

namespace casefile.application.UseCases.SchemaClientUseCases;

public class UpdateSchemaClient(
    ISchemaClientRepository schemaClientRepository,
    IValidator<UpdateSchemaClientDto> validator,
    IMapper mapper) : IUpdateSchemaClient
{
    public async Task<Result<SchemaClientDto>> ExecuteAsync(UpdateSchemaClientDto updateSchemaClientDto)
    {
        await validator.ValidateAndThrowAsync(updateSchemaClientDto);

        var existingResult = await schemaClientRepository.GetAllAsync(
            new SchemaClientByIdWithDefinitionsSpecification(updateSchemaClientDto.Id));
        if (existingResult.IsFailed)
        {
            return Result.Fail<SchemaClientDto>(existingResult.Errors);
        }

        var schemaClient = existingResult.Value.FirstOrDefault();
        if (schemaClient is null)
        {
            return Result.Fail<SchemaClientDto>(
                $"SchemaClient introuvable pour l'identifiant {updateSchemaClientDto.Id}.");
        }

        schemaClient.Nom = updateSchemaClientDto.Nom;
        schemaClient.Definitions.Clear();
        foreach (var definition in updateSchemaClientDto.CreateDefinitionAttributDtos)
        {
            schemaClient.Definitions.Add(new DefinitionAttribut
            {
                Cle = definition.Cle,
                Libelle = definition.Libelle,
                Type = definition.Type,
                EstRequis = definition.EstRequis,
                ValeurDefaut = definition.ValeurDefaut
            });
        }

        var updateResult = await schemaClientRepository.UpdateAsync(schemaClient);
        if (updateResult.IsFailed)
        {
            return Result.Fail<SchemaClientDto>(updateResult.Errors);
        }

        return Result.Ok(mapper.Map<SchemaClientDto>(schemaClient));
    }
}
