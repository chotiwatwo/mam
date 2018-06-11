using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MamApi.Common
{
    public class FileContentType
    {
        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public Dictionary<string, string> GetMimeTypes()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            List<string> contentTypes = configuration.GetSection("File:ContentTypes").Get<List<string>>();
            Dictionary<string, string> dc = new Dictionary<string, string>();
            contentTypes.ForEach(c =>
            {
                string[] tmp = c.Split(":");
                string ext = tmp[0];
                string contentType = tmp[1];
                dc.Add(ext, contentType);
            });
            return dc;
        }
    }
}
