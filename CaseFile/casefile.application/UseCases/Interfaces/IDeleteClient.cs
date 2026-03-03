using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IDeleteClient
{
    Task<Result<bool>> ExecuteAsync(Guid id);
}
