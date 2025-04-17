using Copier.ViewModels;
using System.Windows;

namespace Copier.Views
{
    public partial class ProgressDialog : Window
    {
        public ProgressDialog(ProgressDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
