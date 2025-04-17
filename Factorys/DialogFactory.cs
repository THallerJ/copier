using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.ViewModels;
using Copier.Views;
using System.Windows;

namespace Copier.Factorys
{
    public class DialogFactory : IDialogFactory
    {
        private readonly IFileCopyManager FileCopyManager;
        private readonly IMessenger Messenger;

        public DialogFactory(IFileCopyManager fileCopyManager, IMessenger messenger)
        {
            FileCopyManager = fileCopyManager;
            Messenger = messenger;
        }

        public bool? ShowDialog(IDialog T)
        {
            return T switch
            {
                CopyJobDialogViewModel => ShowDialogHelper(() => new CopyJobDialogViewModel(FileCopyManager, Messenger), vm => new CopyJobDialog(vm)),
                ProgressDialogViewModel => ShowDialogHelper(() => new ProgressDialogViewModel(), vm => new ProgressDialog(vm)),
                _ => throw new ArgumentException("Unknown dialog type")
            };
        }

        private static bool? ShowDialogHelper<T>(Func<T> vmFactory, Func<T, Window> dialogFactory) where T : IDialog
        {
            var vm = vmFactory();
            var dialog = dialogFactory(vm);
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