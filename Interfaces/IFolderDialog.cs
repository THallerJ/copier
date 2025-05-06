namespace Copier.Interfaces
{
    public interface IFolderDialog
    {
        string? FolderName { get; set; }

        bool SelectFolder();
    }
}
