using System.Threading.Tasks;
using casefile.domain.model;

namespace casefile.desktop.Services;

public interface IDialogWindowService
{
    /// <summary>
    /// Permet de lancer la fenetre pour creer des template de dossier.
    /// </summary>
    /// <returns></returns>
    Task<TemplateDossier?> ShowCreateTemplateDossierDialog();
}