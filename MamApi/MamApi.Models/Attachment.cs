using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class Attachment
    {
        [Required]
        [Column("Attachment_ID")]
        public int Id { get; set; }

        [Required]
        [Column("Attachment_AppID")]
        public string AppId { get; set; }

        [Required]
        [Column("Attachment_CustID")]
        public int CustomerId { get; set; }

        [Column("Attachment_CCCreditChkID")]
        public long? CCCreditChkId { get; set; }

        [Column("Attachment_Type")]
        public string AttachmentType { get; set; }

        // แสดงชื่อ Description ของประเภทการแนบ เช่น C = 'Consent'
        public string AttachmentTypeName { get; set; }

        [Column("Attachment_Name")]
        public string Name { get; set; }

        [Column("Attachment_FileName")]
        public string FileName { get; set; }

        [Column("Attachment_Remark")]
        public string Remark { get; set; }

        [Column("Attachment_Status")]
        public string Status { get; set; }

        [Column("Attachment_CreateBy")]
        public string CreateBy { get; set; }

        [Column("Attachment_CreateDate")]
        public DateTime? CreateDate { get; set; }

        [Column("Attachment_UpdateBy")]
        public string UpdateBy { get; set; }

        [Column("Attachment_UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        // สำหรับแสดงผล Path ของ File ที่ Upload ไปแล้ว  (ex. //files.cal.co.th:8888/CreditChecking/0161000001/xxx.jpg)
        public string DisplayFilePath { get; set; } 
    }
}
