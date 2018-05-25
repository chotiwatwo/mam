using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    // Class สำหรับรับค่าเข้ามา เพื่อเอาไปใช้ตั้งชื่อ file ที่ attach มา
    public class AttachmentInfo
    {
        private string _attachmentTypeName = string.Empty;

        public string AppId { get; set; }

        public int CustomerId { get; set; }

        public string AttachmentType { get; set; }

        public string AttachmentTypeName
        {
            get
            {
                return _attachmentTypeName;
            }

            set
            {
                _attachmentTypeName = value;
            }
        }
    }
}
