using casefile.application.DTOs.DocumentClient;
using FluentResults;

namespace casefile.application.Services;

public interface IClientDocumentUploadService
{
    Task<Result<UploadClientDocumentResultDto>> ExecuteAsync(UploadClientDocumentRequestDto request);
}
