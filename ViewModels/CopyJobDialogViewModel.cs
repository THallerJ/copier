using CommunityToolkit.Mvvm.Input;
using Copier.Interfaces;

namespace Copier.ViewModels
{
    public partial class CopyJobDialogViewModel : ISubmittableDialog
    {
        public event EventHandler? OnCancel;
        public event EventHandler? OnOk;
        private readonly IFileCopyManager FileCopyManager;

        public CopyJobDialogViewModel(IFileCopyManager fileCopyManager)
        {
            FileCopyManager = fileCopyManager;
        }

        [RelayCommand]
        public async Task Save(string name)
        {
            await FileCopyManager.SaveCopyJobAsync(name);
            OnOk?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        public void Cancel()
        {
            OnCancel?.Invoke(this, EventArgs.Empty);
        }
    }
}
