namespace CopierTests.Mocks
{
    public class File
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public FileAttributes Attribute { get; set; }

        public File(string name, string text, FileAttributes attribute)
        {
            Name = name;
            Attribute = attribute;
            Text = text;
        }
    }
}
