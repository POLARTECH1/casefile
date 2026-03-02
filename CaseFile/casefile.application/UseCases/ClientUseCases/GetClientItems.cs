using casefile.application.DTOs.Client;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.Clients;
using FluentResults;

namespace casefile.application.UseCases.ClientUseCases;

/// <summary>
/// Recupere la liste des clients et leurs metriques pour l'affichage.
/// </summary>
public class GetClientItems(IClientRepository clientRepository) : IGetClientItems
{
    public async Task<Result<IEnumerable<ClientItemDto>>> ExecuteAsync(ClientItemFilterDto? filtre = null)
    {
        var clientsResult = await clientRepository.GetAllAsync(new ClientWithFiltersSpecification(
            filtre?.NomPrenom,
            filtre?.Email,
            filtre?.NomSchema,
            filtre?.NombreDocuments));
        if (clientsResult.IsFailed)
        {
            return Result.Fail<IEnumerable<ClientItemDto>>(clientsResult.Errors);
        }

        var items = clientsResult.Value
            .Select(client => new ClientItemDto
            {
                Id = client.Id,
                NomPrenom = $"{client.Prenom} {client.Nom}".Trim(),
                Email = client.Email,
                NomSchema = client.SchemaClient?.Nom ?? string.Empty,
                NombreDocuments = client.Dossiers.Sum(d => d.Documents.Count)
            })
            .OrderBy(client => client.NomPrenom);

        return Result.Ok<IEnumerable<ClientItemDto>>(items);
    }
}
