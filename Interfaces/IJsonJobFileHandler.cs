namespace Copier.Interfaces
{
    public interface IJsonJobFileHandler
    {
        Task<List<IJob<T>>> WriteAsync<T>(string fileName, IJob<T> data);

        Task<List<IJob<T>>> ReadAsync<T>(string filename);

        List<IJob<T>> Read<T>(string filenamec);

        void DeleteAllData();

        Task<List<IJob<T>>> DeleteAsync<T>(string filename, string id);
    }
}
