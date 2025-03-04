using CommunityToolkit.Mvvm.ComponentModel;

namespace Copier.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        public SelectFromFolderViewModel selectFromFolderViewModel;

        [ObservableProperty]
        public SelectToFolderViewModel selectToFolderViewModel;

        [ObservableProperty]
        public ActionPanelViewModel actionPanelViewModel;

        public MainWindowViewModel( SelectFromFolderViewModel _selectFromViewModel, 
                SelectToFolderViewModel _selectToFolderView,
                ActionPanelViewModel _actionPanelViewModel)
        {
            selectFromFolderViewModel = _selectFromViewModel;
            selectToFolderViewModel = _selectToFolderView;
            actionPanelViewModel = _actionPanelViewModel;
        }
    }
}
