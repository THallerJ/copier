namespace Copier.Interfaces
{
    public interface IShortcutManager 
    {
       Task<bool> CreateCopyShortcut(string name, string src, string dest);
    }
}
