using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using casefile.desktop.ViewModels.WindowModal;

namespace casefile.desktop.Views.WindowModal;

public partial class ConfirmDialogWindow : Window
{
    public ConfirmDialogWindow()
    {
        InitializeComponent();
    }

    public ConfirmDialogWindow(ConfirmDialogWindowViewModel viewModel)
    {
        DataContext = viewModel;
        viewModel.CloseRequested += result => Close(result);
        InitializeComponent();
    }
}