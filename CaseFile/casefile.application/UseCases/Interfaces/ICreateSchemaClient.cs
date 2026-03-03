using casefile.application.DTOs.SchemaClient;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface ICreateSchemaClient
{
    Task<Result<SchemaClientDto>> ExecuteAsync(CreateSchemaClientDto createSchemaClientDto);
}
