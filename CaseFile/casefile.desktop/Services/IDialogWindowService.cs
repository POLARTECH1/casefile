using System;
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

    /// <summary>
    /// Affiche une boîte de dialogue de confirmation.
    /// Retourne un booléen indiquant la décision de l'utilisateur, ou null si la boîte de dialogue est fermée sans réponse explicite.
    /// </summary>
    /// <param name="message">Le message affiché dans la boîte de dialogue.</param>
    /// <param name="confirmButtonText">Le texte du bouton de confirmation. Peut être null pour utiliser la valeur par défaut.</param>
    /// <param name="cancelButtonText">Le texte du bouton d'annulation. Peut être null pour utiliser la valeur par défaut.</param>
    /// <param name="title">Le titre de la boîte de dialogue. Peut être null affichera "COnfirmation" comme valeur pardefaut</param>
    /// <param name="result">Valeur booléenne initiale à passer à la boîte de dialogue. Peut être null.</param>
    /// <param name="closeRequested">Action facultative déclenchée lors de la fermeture de la boîte de dialogue avec une valeur sélectionnée.</param>
    /// <returns>Une tâche qui résout un booléen indiquant la sélection de l'utilisateur (true pour confirmer, false pour annuler), ou null si aucune sélection n'a été effectuée.</returns>
    Task<bool?> ShowConfirmationDialog(string message, string? confirmButtonText = null,
        string? cancelButtonText = null, string? title = null, bool? result = null,
        Action<bool?>? closeRequested = null);
}