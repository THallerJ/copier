using Copier.Interfaces;

namespace Copier.ViewModels
{
    public class ProgressDialogViewModel : IDialog
    {
        public event EventHandler? OnCancel;
        public event EventHandler? OnOk;
    }
}
