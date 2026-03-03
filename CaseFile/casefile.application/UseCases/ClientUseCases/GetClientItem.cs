using casefile.application.DTOs.Client;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.Clients;
using FluentResults;

namespace casefile.application.UseCases.ClientUseCases;

public class GetClientItem(IClientRepository clientRepository) : IGetClientItem
{
    public async Task<Result<ShowClientDto>> ExecuteAsync(Guid id)
    {
        var clientsResult = await clientRepository.GetAllAsync(new ClientByIdWithDetailsSpecification(id));
        if (clientsResult.IsFailed)
        {
            return Result.Fail<ShowClientDto>(clientsResult.Errors);
        }

        var client = clientsResult.Value.FirstOrDefault();
        if (client is null)
        {
            return Result.Fail<ShowClientDto>($"Client introuvable pour l'identifiant {id}.");
        }

        return Result.Ok(new ShowClientDto
        {
            Id = client.Id,
            Nom = client.Nom,
            Prenom = client.Prenom,
            Telephone = client.Telephone,
            Email = client.Email,
            NomSchema = client.SchemaClient?.Nom ?? string.Empty,
            NombreDocuments = client.Dossiers.Sum(d => d.Documents.Count)
        });
    }
}
