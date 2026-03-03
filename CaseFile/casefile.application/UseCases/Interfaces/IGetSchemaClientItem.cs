using casefile.application.DTOs.SchemaClient;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetSchemaClientItem
{
    Task<Result<SchemaClientItemDto>> ExecuteAsync(Guid id);
}
