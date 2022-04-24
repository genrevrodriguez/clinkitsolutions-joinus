using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tusdotnet.Models;

namespace Fleet.Files.ViewModels
{
    public class FileViewModel
    {
        public string Id { get; set; }

        public Dictionary<string, string> Metadatas { get; set; }
    }
}
