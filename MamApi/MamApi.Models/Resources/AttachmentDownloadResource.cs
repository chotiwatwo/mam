using System;
using System.Collections.Generic;
using System.Text;

namespace MamApi.Models.Resources
{
    public class AttachmentDownloadResource
    {
        public string AppId { get; set; }

        public string Category { get; set; }

        public int CustomerId { get; set; }

        public string AttachmentType { get; set; }

        public string AttachmentTypeName { get; set; }

        public string Name { get; set; }

        public string DisplayFilePath { get; set; }
    }
}
