using Copier.ViewModels;
using Copier.Views;

namespace Copier.Config
{
    class DialogDictionary
    {
        public static readonly Dictionary<Type, Type> Dialogs = new Dictionary<Type, Type>{
            { typeof(CopyJobDialogViewModel), typeof(CopyJobDialog) },
            { typeof(CopyProgressDialogViewModel), typeof(CopyProgressDialog) }
        };
    }
}
