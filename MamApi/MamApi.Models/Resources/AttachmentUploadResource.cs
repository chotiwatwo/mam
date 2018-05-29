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

    /* 
        {
            "appId": "9961000102",
            "customerId": 383790,
            "attachmentType": "F",
            "attachmentTypeName": "คำเสนอขอเช่าซื้อ ด้านหน้า",
            "name": "9961000102_383790_คำเสนอขอเช่าซื้อ ด้านหน้า.jpg",
            "displayFilePath": "//files.cal.co.th:8888/CreditChecking/9961000102/9961000102_383790_คำเสนอขอเช่าซื้อ ด้านหน้า.jpg"
        }
     
     */
}
