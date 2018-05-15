using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamApi.Models
{
    public class MktApplicationExtend
    {
        [Key]
        [Column("MKT_ApplicationExtend_AppID")]
        public string AppId { get; set; }

        //public MktApplication MktApp { get; set; }

        [Column("MKT_ApplicationExtend_OwnerBranchID")]
        public string OwnerBranchId { get; set; }

        [Column("MKT_ApplicationExtend_PreviousBy")]
        public string PreviousBy { get; set; }

        [Column("MKT_ApplicationExtend_PreviousDate")]
        public DateTime? PreviousDate { get; set; }

        [Column("MKT_ApplicationExtend_PreviousLevel")]
        public string PreviousLevel { get; set; }

        [Column("MKT_ApplicationExtend_CurrentBy")]
        public string CurrentBy { get; set; }

        [Column("MKT_ApplicationExtend_CurrentLevel")]
        public string CurrentLevel { get; set; }

        [Column("MKT_ApplicationExtend_EndAppType")]
        public string EndAppType { get; set; }

        [Column("MKT_ApplicationExtend_EndRemarkID")]
        public string EndRemarkID { get; set; }

        [Column("MKT_ApplicationExtend_EndRemark")]
        public string EndRemark { get; set; }

        [Column("MKT_ApplicationExtend_SaleSupportID")]
        public string SaleSupportID { get; set; }

        [Column("MKT_ApplicationExtend_MarketingCenterID")]
        public string MarketingCenterID { get; set; }

        [Column("MKT_ApplicationExtend_MarketingCenterReceiveDoc")]
        public DateTime? MarketingCenterReceiveDoc { get; set; }

        [Column("MKT_ApplicationExtend_MarketingCenterSendToVer")]
        public DateTime? MarketingCenterSendToVer { get; set; }

        [Column("MKT_ApplicationExtend_MKTSendToMS")]
        public DateTime? MKTSendToMS { get; set; }

    }
}
