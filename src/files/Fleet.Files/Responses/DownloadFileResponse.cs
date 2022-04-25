using Fleet.Files.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Files.Responses
{
    public class DownloadFileResponse
    {
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
}
