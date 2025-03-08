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

        [ObservableProperty]
        public SidebarViewModel sidebarViewModel;

        [ObservableProperty]
        public TopMenuViewModel topMenuViewModel;

        public MainWindowViewModel(SelectFromFolderViewModel _selectFromViewModel, SelectToFolderViewModel _selectToFolderView, ActionPanelViewModel _actionPanelViewModel, SidebarViewModel _sidebarViewModel, TopMenuViewModel _topMenuViewModel)
        {
            selectFromFolderViewModel = _selectFromViewModel;
            selectToFolderViewModel = _selectToFolderView;
            actionPanelViewModel = _actionPanelViewModel;
            sidebarViewModel = _sidebarViewModel;
            topMenuViewModel = _topMenuViewModel;
        }
    }
}