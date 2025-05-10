using Copier.Interfaces;
using Microsoft.Win32;

namespace Copier.Services
{
    public class FolderDialog : IFolderDialog
    {
        public string? FolderName { get; set; }

        public bool SelectFolder()
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Multiselect = false;
            var result = folderDialog.ShowDialog() ?? false;
            FolderName = folderDialog.FolderName;
            return result;
        }
    }
}
