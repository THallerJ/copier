using Copier.ViewModels;
using System.Windows;

namespace Copier.Views
{
    public partial class CopyProgressDialog : Window
    {
        public CopyProgressDialog(CopyProgressDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
