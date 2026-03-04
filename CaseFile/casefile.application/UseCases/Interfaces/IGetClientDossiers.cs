using casefile.application.DTOs.Client;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetClientDossiers
{
    Task<Result<IEnumerable<ShowClientDossierDto>>> ExecuteAsync(Guid clientId);
}
