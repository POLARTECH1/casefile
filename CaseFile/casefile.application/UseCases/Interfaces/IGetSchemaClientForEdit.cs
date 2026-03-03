using casefile.application.DTOs.SchemaClient;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetSchemaClientForEdit
{
    Task<Result<UpdateSchemaClientDto>> ExecuteAsync(Guid id);
}
