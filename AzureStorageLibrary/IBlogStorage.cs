using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureStorageLibrary
{
    public enum EcontainerName
    {
        pictures,
        pdf,
        logs
    }
    public interface IBlogStorage
    {
        public string BlobUrl { get; }

        Task UploadAsync(Stream fileStream, string fileName, EcontainerName econtainerName);
        Task<Stream> DownloadAsync(string fileName, EcontainerName econtainerName);
        Task DeleteAsync(string fileName, EcontainerName econtainerName);
        Task SetLogAsync(string text, string fileName);
        Task<List<string>> GetLogAsync(string fileName);
        List<string> GetNames(EcontainerName econtainerName);
    }
}