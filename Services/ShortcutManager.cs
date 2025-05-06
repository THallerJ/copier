using Copier.Interfaces;
using System.IO;

namespace Copier.Services
{
    public class ShortcutManager : IShortcutManager
    {
        public async Task<bool> CreateCopyShortcut(string name, string src, string dest)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (File.Exists(Path.Combine(desktopPath, $"{name}.bat")) == true) return false;

            string batScript = $"@echo off{Environment.NewLine}robocopy \"{src}\" \"{dest}\" /mir /e /s{Environment.NewLine}pause";

            await File.WriteAllTextAsync(Path.Combine(desktopPath, $"{name}.bat"), batScript);
            return true;
        }
    }
}
