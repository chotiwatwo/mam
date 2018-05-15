using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class MktAnnotation
    {
        [Key]
        [Column("Annotation_AppID")]
        public string AppId { get; set; }

        [Column("Annotation_ApproveFlag")]
        public string ApproveFlag { get; set; }

        [Column("Annotation_ApproveRemark")]
        public string ApproveRemark { get; set; }

        [Column("Annotation_MKTAnnoID")]
        public string MktAnnoId { get; set; }

        [Column("Annotation_MKTAnnoRemark")]
        public string MktAnnoRemark { get; set; }

        [Column("Annotation_MKTSupportAnnoID")]
        public string MktSupportAnnoId { get; set; }

        [Column("Annotation_MKTSupportAnnoRemark")]
        public string MktSupportAnnoRemark { get; set; }

        [Column("Annotation_MKTCenterAnnoID")]
        public string MktCenterAnnoId { get; set; }

        [Column("Annotation_MKTCenterAnnoRemark")]
        public string MktCenterAnnoRemark { get; set; }

        [Column("Annotation_VERAssignAnnoID")]
        public string VerAssignAnnoId { get; set; }
       
        [Column("Annotation_VERAssignAnnoRemark")]
        public string VerAssignAnnoRemark { get; set; }

        [Column("Annotation_VERKeyAnnoID")]
        public string VerKeyAnnoId { get; set; }

        [Column("Annotation_VERKeyAnnoRemark")]
        public string VerKeyAnnoRemark { get; set; }

        [Column("Annotation_VERApproveAnnoID")]
        public string VerApproveAnnoId { get; set; }

        [Column("Annotation_VERApproveAnnoRemark")]
        public string VerApproveAnnoRemark { get; set; }

        [Column("Annotation_MKTAppealAnnoID")]
        public string MktAppealAnnoId { get; set; }

        [Column("Annotation_MKTAppealAnnoRemark")]
        public string MktAppealAnnoRemark { get; set; }

        [Column("Annotation_OperAnnoID")]
        public string OperAnnoId { get; set; }

        [Column("Annotation_OperAnnoRemark")]
        public string OperAnnoRemark { get; set; }

        [Column("Annotation_Status")]
        public string Status { get; set; }

        [Column("Annotation_CreateBy")]
        public string CreateBy { get; set; }

        [Column("Annotation_CreateDate")]
        public DateTime? CreateDate { get; set; }

        [Column("Annotation_UpdateBy")]
        public string UpdateBy { get; set; }

        [Column("Annotation_UpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
