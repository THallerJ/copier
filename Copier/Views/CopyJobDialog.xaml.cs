using Copier.ViewModels;
using System.Windows;

namespace Copier.Views
{
    public partial class CopyJobDialog : Window
    {
        public CopyJobDialog(CopyJobDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}