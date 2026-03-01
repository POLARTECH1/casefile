using AutoMapper;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.domain.model;
using FluentResults;
using FluentValidation;

namespace casefile.application.UseCases.SchemaClientUseCases;

/// <summary>
/// Classe CreateSchemaClient utilisée pour gérer la logique métier liée à la création d'un SchemaClient.
/// Cette classe implémente l'interface ICreateSchemaClient afin de permettre l'exécution du cas d'utilisation correspondant.
/// </summary>
/// <remarks>
/// La classe est responsable de valider les données d'entrée, de mapper les DTOs vers les entités correspondantes,
/// et d'utiliser le référentiel pour l'ajout des données dans le stockage persistant.
/// </remarks>
/// <param name="schemaClientRepository">Dépendance utilisée pour interagir avec le référentiel des SchemaClients. Permet d'ajouter un nouveau SchemaClient au stockage.</param>
/// <param name="validator">Instance destinée à valider les objets CreateSchemaClientDto en fonction des règles prédéfinies.</param>
/// <param name="mapper">Composant AutoMapper utilisé pour transformer les entités SchemaClient en objets DTO pour les réponses ou autres manipulations.</param>
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