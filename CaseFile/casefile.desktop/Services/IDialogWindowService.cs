using System.Threading.Tasks;

namespace casefile.desktop.Services;

/// <summary>
/// Interface qui définit un service pour afficher une fenêtre de dialogue.
/// </summary>
/// <typeparam name="TRequest">Type de la requête utilisée pour configurer la fenêtre de dialogue.</typeparam>
/// <typeparam name="TResult">Type du résultat retourné après la fermeture de la fenêtre de dialogue.</typeparam>
public interface IDialogWindowService<in TRequest, TResult>
{
    Task<TResult> Show(TRequest request);
}
