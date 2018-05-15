using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class ApplicationLog
    {
        [Key]
        [Column("ApplicationLog_ID")]
        public long AppLogId { get; set; }

        [Column("ApplicationLog_AppID")]
        public string AppId { get; set; }

        [Column("ApplicationLog_FromUserID")]
        public string FromUserId { get; set; }

        [Column("ApplicationLog_FromLevelID")]
        public string FromLevelId { get; set; }

        [Column("ApplicationLog_FromDepartmentID")]
        public string FromDepartmentID { get; set; }

        [Column("ApplicationLog_FromBranchID")]
        public string FromBranchID { get; set; }

        [Column("ApplicationLog_Date")]
        public DateTime? AppLogDate { get; set; }

        [Column("ApplicationLog_AppStatus")]
        public string AppStatus { get; set; }

        [Column("ApplicationLog_RemarkID")]
        public string RemarkId { get; set; }

        [Column("ApplicationLog_Remark")]
        public string Remark { get; set; }

        [Column("ApplicationLog_ActionName")]
        public string ActionName { get; set; }

        [Column("ApplicationLog_Status")]
        public string Status { get; set; }

        [Column("ApplicationLog_CreateBy")]
        public string CreateBy { get; set; }

        [Column("ApplicationLog_CreateDate")]
        public DateTime? CreateDate { get; set; }

    }
}
