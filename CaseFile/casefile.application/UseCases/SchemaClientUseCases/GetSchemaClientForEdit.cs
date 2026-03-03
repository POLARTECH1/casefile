using casefile.application.DTOs.DefinitionAttribut;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.SchemasClients;
using FluentResults;

namespace casefile.application.UseCases.SchemaClientUseCases;

/// <summary>
/// Classe responsable de récupérer les informations d'un SchemaClient pour modification.
/// </summary>
/// <remarks>
/// Cette classe implémente le cas d'utilisation permettant de récupérer un SchemaClient existant en fonction de son identifiant et
/// de transformer celui-ci en un DTO utilisé pour l'édition. Elle repose sur un repository pour accéder aux données.
/// </remarks>
/// <param name="schemaClientRepository">
/// Instance de ISchemaClientRepository permettant de récupérer les données du SchemaClient.
/// </param>
/// <example>
/// Cette classe est utilisée dans des scénarios où un utilisateur souhaite modifier les propriétés d'un SchemaClient, y compris
/// ses définitions associées. Elle renvoie un objet UpdateSchemaClientDto contenant l'ensemble des informations nécessaires pour l'édition.
/// </example>
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
