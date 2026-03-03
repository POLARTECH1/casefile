using casefile.application.DTOs.Client;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

/// <summary>
/// Crée un nouveau client avec ses attributs et associations optionnelles.
/// </summary>
public interface ICreateClient
{
    Task<Result<Guid>> ExecuteAsync(CreateClientDto dto);
}
