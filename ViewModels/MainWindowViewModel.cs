using CommunityToolkit.Mvvm.ComponentModel;

namespace Copier.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        public SelectSrcFolderViewModel selectSrcFolderViewModel;

        [ObservableProperty]
        public SelectDestFolderViewModel selectDestFolderViewModel;

        [ObservableProperty]
        public ActionPanelViewModel actionPanelViewModel;

        [ObservableProperty]
        public SidebarViewModel sidebarViewModel;

        [ObservableProperty]
        public TopMenuViewModel topMenuViewModel;

        public MainWindowViewModel(SelectSrcFolderViewModel _selectSrcViewModel, SelectDestFolderViewModel _selectDestFolderView, ActionPanelViewModel _actionPanelViewModel, SidebarViewModel _sidebarViewModel, TopMenuViewModel _topMenuViewModel)
        {
            selectSrcFolderViewModel = _selectSrcViewModel;
            selectDestFolderViewModel = _selectDestFolderView;
            actionPanelViewModel = _actionPanelViewModel;
            sidebarViewModel = _sidebarViewModel;
            topMenuViewModel = _topMenuViewModel;
        }
    }
}