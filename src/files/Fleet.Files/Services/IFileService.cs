using Fleet.Files.Requests;
using Fleet.Files.Responses;

namespace Fleet.Files.Services
{
    public interface IFileService
    {
        IEnumerable<string> GetFileIds();
        Task<GetFilesResponse> GetFilesAsync(GetFilesRequest request, CancellationToken cancellationToken = default);
        Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request, CancellationToken cancellationToken = default);
    }
}