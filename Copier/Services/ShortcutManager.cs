using Copier.Interfaces;
using System.IO;

namespace Copier.Services
{
    public class ShortcutManager : IShortcutManager
    {
        private readonly IFileService FileService;

        public ShortcutManager(IFileService fileService)
        {
            FileService = fileService;
        }

        public async Task<bool> CreateCopyShortcut(string name, string src, string dest)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (FileService.Exists(Path.Combine(desktopPath, $"{name}.bat")) == true) return false;

            string batScript = $"@echo off{Environment.NewLine}robocopy \"{src}\" \"{dest}\" /mir /e /s{Environment.NewLine}pause";

            await FileService.WriteAllTextAsync(Path.Combine(desktopPath, $"{name}.bat"), batScript);
            return true;
        }
    }
}
