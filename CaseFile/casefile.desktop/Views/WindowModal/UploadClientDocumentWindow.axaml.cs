using Avalonia.Controls;
using casefile.desktop.ViewModels.WindowModal;

namespace casefile.desktop.Views.WindowModal;

public partial class UploadClientDocumentWindow : Window
{
    public UploadClientDocumentWindow()
    {
        InitializeComponent();
    }

    public UploadClientDocumentWindow(UploadClientDocumentWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseRequested += result => Close(result);
    }
}
