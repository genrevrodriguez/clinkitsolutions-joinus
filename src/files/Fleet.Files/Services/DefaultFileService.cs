using Fleet.Files.Helpers;
using Fleet.Files.Requests;
using Fleet.Files.Responses;
using Fleet.Files.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tusdotnet.Interfaces;
using tusdotnet.Stores;

namespace Fleet.Files.Services
{
    public class DefaultFileService : IFileService
    {
        private readonly IConfiguration configuration;
        private readonly TusDiskStore tusDiskStore;

        public DefaultFileService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.tusDiskStore = TusHelper.GetStore(configuration);
        }

        public IEnumerable<string> GetFileIds()
        {
            return Directory
                .EnumerateFiles(TusHelper.GetStorePath(this.configuration), "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => string.IsNullOrWhiteSpace(Path.GetExtension(f)))
                .Select(f => Path.GetFileName(f));
        }

        public async Task<GetFilesResponse> GetFilesAsync(GetFilesRequest request, CancellationToken cancellationToken = default)
        {
            var fileIds = string.IsNullOrWhiteSpace(request?.FileId) ? this.GetFileIds() : new[] { request.FileId };
            var fileTasks = fileIds.Select(fileId => this.tusDiskStore.GetFileAsync(fileId, cancellationToken));
            var files = await Task.WhenAll(fileTasks);

            var fileVmTasks = files.Select(async file => new FileViewModel()
            {
                Id = file.Id,
                Metadatas = (await file.GetMetadataAsync(cancellationToken))
                    .ToDictionary(m => m.Key, m => m.Value.GetString(Encoding.UTF8))
            });

            return new GetFilesResponse()
            {
                Files = (await Task.WhenAll(fileVmTasks))
                    .OrderBy(f => DateTime.TryParse(f.Metadatas.GetValueOrDefault("date"), out var result) ? result : default(DateTime))
            };
        }

        public async Task<DownloadFileResponse> DownloadFileAsync(DownloadFileRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request?.FileId)) return null;

            var file = await this.tusDiskStore.GetFileAsync(request.FileId, cancellationToken);
            if (file == null) return null;

            return new DownloadFileResponse()
            {
                FileName = (await file.GetMetadataAsync(cancellationToken)).GetValueOrDefault("filename")?.GetString(Encoding.UTF8),
                FileStream = await file.GetContentAsync(cancellationToken)
            };
        }

        public async Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request?.FileId)) return null;

            await this.tusDiskStore.DeleteFileAsync(request.FileId, cancellationToken);

            return new DeleteFileResponse();
        }
    }
}
