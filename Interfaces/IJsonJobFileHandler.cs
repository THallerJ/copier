namespace Copier.Interfaces
{
    public interface IJsonJobFileHandler
    {
        Task WriteAsync<T>(string fileName, IJob<T> data);

        Task<List<IJob<T>>> ReadAsync<T>(string filename);

        void DeleteAllData();
    }
}
