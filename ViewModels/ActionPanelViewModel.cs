using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Copier.Interfaces;

namespace Copier.ViewModels
{
    public partial class ActionPanelViewModel : ObservableObject
    {
        private readonly IDialogFactory DialogFactory;
        private readonly CopyJobDialogViewModel CopyJobDialogViewModel;
        private readonly CopyProgressDialogViewModel CopyProgressDialogViewModel;

        public ActionPanelViewModel(IDialogFactory dialogFactory, CopyJobDialogViewModel copyJobDialogViewModel, CopyProgressDialogViewModel copyProgressDialogViewModel)
        {
            DialogFactory = dialogFactory;
            CopyJobDialogViewModel = copyJobDialogViewModel;
            CopyProgressDialogViewModel = copyProgressDialogViewModel;
        }

        [RelayCommand]
        public void CopyFiles()
        {
            DialogFactory.ShowDialog(CopyProgressDialogViewModel);
        }

        [RelayCommand]
        public void SaveCopyJob()
        {
            DialogFactory.ShowDialog(CopyJobDialogViewModel);
        }
    }
}
