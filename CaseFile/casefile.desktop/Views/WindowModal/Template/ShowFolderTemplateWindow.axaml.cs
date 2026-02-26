using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using casefile.desktop.ViewModels.Template;

namespace casefile.desktop.Views.WindowModal.Template;

public partial class ShowFolderTemplateWindow : Window
{
    public ShowFolderTemplateWindow()
    {
        InitializeComponent();
    }

    public ShowFolderTemplateWindow(ShowFolderTemplateWindowViewModel viewModel)
    {
        DataContext = viewModel;
    }
}