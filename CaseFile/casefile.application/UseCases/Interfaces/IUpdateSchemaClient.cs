using casefile.application.DTOs.SchemaClient;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IUpdateSchemaClient
{
    Task<Result<SchemaClientDto>> ExecuteAsync(UpdateSchemaClientDto updateSchemaClientDto);
}
