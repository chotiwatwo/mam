using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class AttachmentUploadResource
    {
        public string AppId { get; set; }

        public int CustomerId { get; set; }

        public string AttachmentType { get; set; }

        public string AttachmentTypeName { get; set; }

        public string Name { get; set; }

        public string DisplayFilePath { get; set; }
    }
}
