namespace Copier.Interfaces
{
    public interface ICopyManager
    {
        string? Name
        {
            get;
            set;
        }

        string? FromPath
        {
            get;
            set;
        }

        string? ToPath
        {
            get;
            set;
        }

        void runCopy();

        void runCopy(string fromPath, string toPath);

        void SaveCopy(string name);

        void GetCopy(string name);
    }
}
