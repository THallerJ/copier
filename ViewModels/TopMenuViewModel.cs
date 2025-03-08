using CommunityToolkit.Mvvm.Input;
using Copier.Interfaces;

namespace Copier.ViewModels
{
    public partial class TopMenuViewModel
    {
        private readonly IJsonJobFileHandler JsonJobFileHandler;

        public TopMenuViewModel(IJsonJobFileHandler jsonJobFileHandler)
        {
            JsonJobFileHandler = jsonJobFileHandler;
        }

        [RelayCommand]
        public void ClearData()
        {
            JsonJobFileHandler.DeleteAllData();
        }
    }
}