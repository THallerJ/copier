using Copier.Interfaces;
using Copier.ViewModels;
using Copier.Views;
using System.Windows;

namespace Copier.Factorys
{
    public class DialogFactory : IDialogFactory
    {
        public bool? ShowDialog<T>(T vm) where T : IDialog
        {
            return vm switch
            {
                CopyJobDialogViewModel copyJobDialogViewModel => ShowDialogHelper(copyJobDialogViewModel, new CopyJobDialog(copyJobDialogViewModel)),
                CopyProgressDialogViewModel copyProgressDialogViewModel => ShowDialogHelper(copyProgressDialogViewModel, new ProgressDialog(copyProgressDialogViewModel)),
                _ => throw new ArgumentException("Unknown dialog type")
            };
        }

        private static bool? ShowDialogHelper<T>(T vm, Window dialog) where T : IDialog
        {
            vm.OnCancel += (s, e) => CloseDialog(dialog, false);
            vm.OnOk += (s, e) => CloseDialog(dialog, true);
            return dialog.ShowDialog();
        }

        private static void CloseDialog(Window dialog, bool ok)
        {
            dialog.DialogResult = ok;
            dialog.Close();
        }
    }
}