using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens
{
    public class AppSettings
    {
        public string AppName { get; set; }
        public string Version { get; set; }
        public string TarkovDatabaseV2BaseUrl { get; set; }
        public string TarkovMarketV1BaseUrl { get; set; }
        public string TarkovToolsGQLUrl { get; set; }
        public string[] AllowedHosts { get; set; }
    }
}
