namespace CopierTests.Mocks
{
    public class FileSystem
    {
        public Dictionary<string, List<File>> FileTree { get; private set; } = [];

        public string FileOutput { get; set; } = "";

        public FileSystem()
        {
            InitFileSystem();
        }

        public void ResetFileSystem()
        {
            InitFileSystem();
            FileOutput = "";
        }

        private void InitFileSystem()
        {
            FileTree = new Dictionary<string, List<File>>();

            // populate src directory
            FileTree["src"] = [];
            for (int i = 1; i <= 5; i++)
            {
                FileAttributes attribute = FileAttributes.Normal;
                if (i % 2 == 0) attribute = FileAttributes.Hidden;
                if (i % 5 == 0) attribute = FileAttributes.System;

                var srcFile = new File($"file{i}.txt", $"text{i}", attribute);
                FileTree["src"].Add(srcFile);
            }

            // populate dest directory
            var destFile = new File("file0.txt", "", FileAttributes.Normal);
            FileTree["dest"] = [destFile];

        }
    }
}
