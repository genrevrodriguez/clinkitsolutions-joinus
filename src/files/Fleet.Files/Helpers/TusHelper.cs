using Fleet.Files.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tusdotnet.Interfaces;
using tusdotnet.Stores;

namespace Fleet.Files.Helpers
{
    public static class TusHelper
    {
        public static TusDiskStore GetStore(IConfiguration configuration) 
            => new TusDiskStore(GetStorePath(configuration));

        public static string GetStorePath(IConfiguration configuration)
            => Path.Combine(AppContext.BaseDirectory, configuration.GetValue<string>("Tus:StorePath"));
    }
}
