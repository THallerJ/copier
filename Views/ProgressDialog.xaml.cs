using Copier.ViewModels;
using System.Windows;

namespace Copier.Views
{
    public partial class ProgressDialog : Window
    {
        public ProgressDialog(CopyProgressDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
