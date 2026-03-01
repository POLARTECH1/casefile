using casefile.application.DTOs.SchemaClient;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetSchemaClientItems
{
    Task<Result<IEnumerable<SchemaClientItemDto>>> ExecuteAsync();
}
