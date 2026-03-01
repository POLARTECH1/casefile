using Avalonia.Controls;
using casefile.desktop.ViewModels.Schema;

namespace casefile.desktop.Views.WindowModal.Schema;

public partial class EditSchemaClientWindow : Window
{
    public EditSchemaClientWindow()
    {
        InitializeComponent();
    }

    public EditSchemaClientWindow(EditSchemaClientWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseRequested += result => Close(result);
    }
}
