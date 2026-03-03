using casefile.application.DTOs.Client;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.domain.model;
using FluentResults;

namespace casefile.application.UseCases.ClientUseCases;

/// <summary>
/// Crée un nouveau client avec ses attributs dynamiques et ses associations optionnelles.
/// </summary>
public class CreateClient(IClientRepository clientRepository) : ICreateClient
{
    public async Task<Result<Guid>> ExecuteAsync(CreateClientDto dto)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            Nom = dto.Nom,
            Prenom = dto.Prenom,
            Telephone = dto.Telephone,
            Email = dto.Email,
            CreeLe = DateTime.UtcNow,
            SchemaClientId = dto.SchemaClientId,
            TemplateDossierId = dto.TemplateDossierId,
            ValeursAttributs = dto.ValeursAttributs.Select(v => new ValeurAttributClient
            {
                Id = Guid.NewGuid(),
                Cle = v.Cle,
                Valeur = v.Valeur
            }).ToList()
        };

        var result = await clientRepository.AddAsync(client);
        if (result.IsFailed)
            return Result.Fail<Guid>(result.Errors);

        return Result.Ok(client.Id);
    }
}
