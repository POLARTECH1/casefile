using Avalonia.Controls;
using casefile.desktop.ViewModels.Template;

namespace casefile.desktop.Views.WindowModal.Template;

public partial class EditFolderTemplateWindow : Window
{
    public EditFolderTemplateWindow(EditFolderTemplateWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseRequested += result => Close(result);
    }
}
