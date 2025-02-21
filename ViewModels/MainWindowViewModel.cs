using CommunityToolkit.Mvvm.ComponentModel;

namespace Copier.ViewModels
{
    public partial class MainWindowViewModel :ObservableObject
    {
        [ObservableProperty]
        public SelectFromFolderViewModel selectFromFolderViewModel;


        [ObservableProperty]
        public SelectToFolderViewModel selectToFolderViewModel;

        public MainWindowViewModel(SelectFromFolderViewModel _selectFromViewModel, SelectToFolderViewModel _selectToFolderView)
        {
            selectFromFolderViewModel = _selectFromViewModel;
            selectToFolderViewModel = _selectToFolderView;
        }
    }
}
