namespace Copier.Interfaces
{
    public interface IJob<T>
    {
        public string? Id { get; set; }

        public T Config { get; }

        public string JobType { get; }
    }
}
