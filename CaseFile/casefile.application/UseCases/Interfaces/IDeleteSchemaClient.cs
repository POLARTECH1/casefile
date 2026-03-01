using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IDeleteSchemaClient
{
    Task<Result<bool>> ExecuteAsync(Guid id);
}
