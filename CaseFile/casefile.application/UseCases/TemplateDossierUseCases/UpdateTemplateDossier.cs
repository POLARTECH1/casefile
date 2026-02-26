using AutoMapper;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.TemplateDossiers;
using casefile.domain.model;
using FluentResults;
using FluentValidation;

namespace casefile.application.UseCases.TemplateDossierUseCases;

/// <summary>
/// Classe définissant le cas d'utilisation pour la mise à jour d'un TemplateDossier.
/// Permet de valider les données d'entrée, de récupérer l'entité existante, de mettre à jour
/// ses propriétés et ses éléments, puis de persister les changements dans le repository.
/// </summary>
public class UpdateTemplateDossier(
    ITemplateDossierRepository templateDossierRepository,
    IValidator<UpdateTemplateDossierDto> validator,
    IMapper mapper) : IUpdateTemplateDossier
{
    public async Task<Result<TemplateDossierDto>> ExecuteAsync(UpdateTemplateDossierDto updateTemplateDossierDto)
    {
        await validator.ValidateAndThrowAsync(updateTemplateDossierDto);

        var existingResult = await templateDossierRepository.GetAllAsync(
            new TemplateDossierByIdWithElementsAndDocumentsSpecification(updateTemplateDossierDto.Id));

        if (existingResult.IsFailed)
        {
            return Result.Fail<TemplateDossierDto>(existingResult.Errors);
        }

        var templateDossier = existingResult.Value.FirstOrDefault();
        if (templateDossier is null)
        {
            return Result.Fail<TemplateDossierDto>(
                $"TemplateDossier introuvable pour l'identifiant {updateTemplateDossierDto.Id}.");
        }

        templateDossier.Nom = updateTemplateDossierDto.Nom;
        templateDossier.Description = updateTemplateDossierDto.Description;

        templateDossier.Elements.Clear();
        foreach (var e in updateTemplateDossierDto.CreateTemplateDossierElementDtos)
        {
            templateDossier.Elements.Add(new TemplateDossierElement()
            {
                Nom = e.Nom,
                IdParent = e.IdParent,
                Ordre = e.Ordre,
                DocumentsAttendus = e.CreateDocumentAttendusDto.Select(d => new DocumentAttendu()
                {
                    IdTypeDocument = d.IdTypeDocument,
                    EstRequis = d.EstRequis
                }).ToList()
            });
        }

        var result = await templateDossierRepository.UpdateAsync(templateDossier);
        if (result.IsFailed)
        {
            return Result.Fail<TemplateDossierDto>(result.Errors);
        }

        return Result.Ok(mapper.Map<TemplateDossierDto>(templateDossier));
    }
}