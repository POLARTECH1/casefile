using casefile.application.DTOs.DocumentAttendu;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.DTOs.TemplateDossierElement;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.TemplateDossiers;
using FluentResults;

namespace casefile.application.UseCases.TemplateDossierUseCases;

/// <summary>
/// Récupère les données complètes d'un template de dossier pour alimenter le formulaire de modification.
/// </summary>
public class GetTemplateDossierForEdit(ITemplateDossierRepository templateDossierRepository) : IGetTemplateDossierForEdit
{
    public async Task<Result<UpdateTemplateDossierDto>> ExecuteAsync(Guid id)
    {
        var result = await templateDossierRepository.GetAllAsync(
            new TemplateDossierByIdWithElementsAndDocumentsSpecification(id));

        if (result.IsFailed)
            return Result.Fail<UpdateTemplateDossierDto>(result.Errors);

        var template = result.Value.FirstOrDefault();
        if (template is null)
            return Result.Fail<UpdateTemplateDossierDto>(
                $"TemplateDossier introuvable pour l'identifiant {id}.");

        var dto = new UpdateTemplateDossierDto
        {
            Id = template.Id,
            Nom = template.Nom,
            Description = template.Description,
            CreateTemplateDossierElementDtos = template.Elements
                .OrderBy(e => e.Ordre)
                .Select(e => new CreateTemplateDossierElementDto
                {
                    Nom = e.Nom,
                    IdParent = e.IdParent,
                    Ordre = e.Ordre,
                    CreateDocumentAttendusDto = e.DocumentsAttendus
                        .Select(d => new CreateDocumentAttenduDto
                        {
                            IdTypeDocument = d.IdTypeDocument,
                            EstRequis = d.EstRequis
                        }).ToList()
                }).ToList()
        };

        return Result.Ok(dto);
    }
}
