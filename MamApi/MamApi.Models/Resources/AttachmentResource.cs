using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class AttachmentResource
    {
        public int Id { get; set; }

        public string AppId { get; set; }

        public int CustomerId { get; set; }

        public long? CCCreditChkId { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public string Remark { get; set; }

        public string Status { get; set; }
    }
}
