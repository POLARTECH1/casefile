using System.Threading.Tasks;
using casefile.application.DTOs.TemplateDossier;

namespace casefile.desktop.Services;

public interface IDialogWindowService
{
    /// <summary>
    /// Ouvre la fenêtre de création d'un template de dossier.
    /// Retourne le template créé, ou null si l'utilisateur annule.
    /// </summary>
    Task<TemplateDossierDto?> ShowCreateTemplateDossierDialog();
}
