using Avalonia.Controls;
using casefile.desktop.ViewModels.Template;

namespace casefile.desktop.Views.WindowModal.Template;

public partial class CreateFolderTemplateWindow : Window
{
    public CreateFolderTemplateWindow(CreateFolderTemplateWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseRequested += result => Close(result);
    }
}
