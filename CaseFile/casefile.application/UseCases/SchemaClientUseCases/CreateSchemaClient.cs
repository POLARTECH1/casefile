using AutoMapper;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.domain.model;
using FluentResults;
using FluentValidation;

namespace casefile.application.UseCases.SchemaClientUseCases;

public class CreateSchemaClient(
    ISchemaClientRepository schemaClientRepository,
    IValidator<CreateSchemaClientDto> validator,
    IMapper mapper) : ICreateSchemaClient
{
    public async Task<Result<SchemaClientDto>> ExecuteAsync(CreateSchemaClientDto createSchemaClientDto)
    {
        await validator.ValidateAndThrowAsync(createSchemaClientDto);

        var schemaClient = new SchemaClient
        {
            Nom = createSchemaClientDto.Nom,
            Definitions = createSchemaClientDto.CreateDefinitionAttributDtos
                .Select(d => new DefinitionAttribut
                {
                    Cle = d.Cle,
                    Libelle = d.Libelle,
                    Type = d.Type,
                    EstRequis = d.EstRequis,
                    ValeurDefaut = d.ValeurDefaut
                }).ToList()
        };

        var result = await schemaClientRepository.AddAsync(schemaClient);
        return result.ToResult<SchemaClientDto>((_) => mapper.Map<SchemaClientDto>(result.Value));
    }
}
