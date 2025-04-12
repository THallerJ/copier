using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Copier.Interfaces;

namespace Copier.ViewModels
{
    public partial class ActionPanelViewModel : ObservableObject
    {
        private readonly IDialogFactory DialogFactory;
        public ActionPanelViewModel(IDialogFactory dialogFactory)
        {
            DialogFactory = dialogFactory;
        }

        [RelayCommand]
        public void CopyFiles()
        {
            DialogFactory.ShowDialog<CopyProgressDialogViewModel>();
        }

        [RelayCommand]
        public void SaveCopyJob()
        {
            DialogFactory.ShowDialog<CopyJobDialogViewModel>();
        }
    }
}
