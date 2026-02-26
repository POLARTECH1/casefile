using System;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.WindowModal;

public partial class ConfirmDialogWindowViewModel : ViewModelBase
{
    [RelayCommand]
    private void Cancel() => CloseRequested?.Invoke(null);

    [RelayCommand]
    private void Confirm() => CloseRequested?.Invoke(true);

    [RelayCommand]
    private void Dismiss() => CloseRequested?.Invoke(false);

    [RelayCommand]
    private void Close() => CloseRequested?.Invoke(null);


    public ConfirmDialogWindowViewModel(string message, string? confirmButtonText = null,
        string? cancelButtonText = null, string? title = null, bool? result = null,
        Action<bool?>? closeRequested = null)
    {
        Message = message;
        ConfirmButtonText = confirmButtonText ?? ConfirmButtonText;
        CancelButtonText = cancelButtonText ?? CancelButtonText;
        Title = title ?? Title;
        Result = result ?? Result;
        CloseRequested = closeRequested ?? CloseRequested;
    }

    /// <summary>
    /// Propriété représentant le titre de la fenêtre du dialogue de confirmation.
    /// Ce titre est affiché en haut de la boîte de dialogue pour indiquer l'objectif général ou le contexte de la confirmation.
    /// </summary>
    public string Title { get; set; } = "Confirmation";

    /// <summary>
    /// Propriété représentant le message principal affiché dans la fenêtre de dialogue de confirmation.
    /// Ce message fournit des détails supplémentaires ou un contexte spécifique à la confirmation demandée.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Propriété représentant le texte affiché sur le bouton de confirmation dans la fenêtre de dialogue.
    /// Ce texte indique l'action à effectuer lorsque l'utilisateur valide la confirmation.
    /// </summary>
    public string? ConfirmButtonText { get; set; } = "Oui";

    /// <summary>
    /// Propriété représentant le texte du bouton d'annulation dans la boîte de dialogue de confirmation.
    /// Ce texte est utilisé pour indiquer l'action permettant de refuser ou d'annuler l'opération en cours.
    /// </summary>
    public string? CancelButtonText { get; set; } = "Non";

    /// <summary>
    /// Propriété représentant le résultat de l'interaction utilisateur avec la boîte de dialogue.
    /// Cette valeur indique si l'utilisateur a confirmé ou annulé l'action proposée.
    /// - `true` : L'utilisateur a confirmé l'action.
    /// - `false` : L'utilisateur a explicitement rejeté l'action.
    /// - `null` : L'utilisateur a annulé ou fermé la boîte de dialogue sans prendre de décision explicite.
    /// </summary>
    public bool? Result { get; set; }

    /// <summary>
    /// Événement déclenché lorsque la fermeture de la fenêtre de dialogue est demandée.
    /// Cet événement permet de signaler la décision prise dans la boîte de dialogue,
    /// par exemple en fonction d'une confirmation, d'une annulation ou d'une autre action.
    /// La valeur transmise peut indiquer le résultat sélectionné ou null si aucune action spécifique n'a été prise.
    /// </summary>
    public event Action<bool?>? CloseRequested;
}