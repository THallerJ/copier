namespace Copier.Interfaces
{
    public interface IJob<T>
    {
        string? Id { get; set; }

        T Config { get; }

        string JobType { get; }
    }
}
