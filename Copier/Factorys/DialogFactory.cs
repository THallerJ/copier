using Copier.Config;
using Copier.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Copier.Factorys
{
    public class DialogFactory : IDialogFactory
    {
        private readonly IServiceProvider Services;
        public DialogFactory(IServiceProvider services)
        {
            Services = services;
        }

        private readonly Dictionary<Type, Type> Dialogs = DialogDictionary.Dialogs;
        public bool? ShowDialog<T>() where T : IDialog
        {
            if (Dialogs.TryGetValue(typeof(T), out var dialogType))
            {
                var vm = Services.GetRequiredService<T>();
                var dialog = (Window?) Activator.CreateInstance(dialogType, vm);
                if (dialog != null) return ShowDialogHelper(vm, dialog);
            }

            throw new ArgumentException("Unknown dialog type");
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