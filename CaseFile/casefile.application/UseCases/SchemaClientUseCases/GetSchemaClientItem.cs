using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.Clients;
using casefile.data.Specifications.SchemasClients;
using FluentResults;

namespace casefile.application.UseCases.SchemaClientUseCases;

/// <summary>
/// Représente une classe permettant de récupérer un élément de type SchemaClient avec ses informations associées.
/// </summary>
/// <remarks>
/// Cette classe implémente l'interface <c>IGetSchemaClientItem</c> et utilise deux repositories :
/// <list type="bullet">
/// <item><description><c>ISchemaClientRepository</c> pour interagir avec les entités SchemaClient.</description></item>
/// <item><description><c>IClientRepository</c> pour compter les clients liés à un SchemaClient donné.</description></item>
/// </list>
/// La méthode <c>ExecuteAsync</c> permet d'exécuter la logique de récupération d'un élément SchemaClient spécifique.
/// </remarks>
/// <param name="schemaClientRepository">
/// Repository utilisé pour interroger les entités SchemaClient à partir de la base de données.
/// </param>
/// <param name="clientRepository">
/// Repository utilisé pour compter le nombre de clients liés à un SchemaClient.
/// </param>
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
