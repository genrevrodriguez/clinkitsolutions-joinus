using Fleet.Files.Requests;
using Fleet.Files.Responses;
using Fleet.Files.Services;
using Fleet.Vehicles.Requests;
using Fleet.Vehicles.Responses;
using Fleet.Vehicles.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Api.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Gets a list of files
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A list of files</returns>
        [HttpGet]
        [Route("")]
        public Task<GetFilesResponse> GetFilesAsync([FromQuery] GetFilesRequest request) => _fileService.GetFilesAsync(request);

        [HttpDelete]
        [Route("")]
        public Task<DeleteFileResponse> DeleteFilesAsync([FromQuery] DeleteFileRequest request) => _fileService.DeleteFileAsync(request);
    }
}
