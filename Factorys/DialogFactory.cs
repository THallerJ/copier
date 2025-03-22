using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.ViewModels;
using Copier.Views;
using System.Windows;

namespace Copier.Factories
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

        public bool? ShowDialog(ISubmittableDialog T)
        {
            if (T.GetType() == typeof(CopyJobDialogViewModel))
            {
                var vm = new CopyJobDialogViewModel(FileCopyManager, Messenger);
                var dialog = new CopyJobDialog(vm);
                InitCloseable(dialog, vm);
                return dialog.ShowDialog();
            }

            throw new ArgumentException("Unknown dialog type");
        }

        private static void InitCloseable(Window dialog, ISubmittableDialog vm)
        {
            vm.OnCancel += (s, e) => CloseDialog(dialog, false);
            vm.OnOk += (s, e) => CloseDialog(dialog, true);
        }

        private static void CloseDialog(Window dialog, bool ok)
        {
            dialog.DialogResult = ok;
            dialog.Close();
        }
    }
}