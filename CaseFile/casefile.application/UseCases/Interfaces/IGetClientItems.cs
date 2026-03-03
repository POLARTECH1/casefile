using casefile.application.DTOs.Client;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetClientItems
{
    Task<Result<IEnumerable<ClientItemDto>>> ExecuteAsync(ClientItemFilterDto? filtre = null);
}
