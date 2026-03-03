using casefile.application.DTOs.Client;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetClientItem
{
    Task<Result<ShowClientDto>> ExecuteAsync(Guid id);
}
