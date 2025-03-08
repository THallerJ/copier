namespace Copier.Interfaces
{
    public interface IFolderDialog
    {
        public string? FolderName { get; set; }

        public bool SelectFolder();
    }
}
