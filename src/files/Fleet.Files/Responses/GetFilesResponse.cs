using Fleet.Files.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Files.Responses
{
    public class GetFilesResponse
    {
        public IEnumerable<FileViewModel> Files { get; set; }
    }
}
