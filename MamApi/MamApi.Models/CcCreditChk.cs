using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamApi.Models
{
    public class CcCreditChk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CreditChk_ID")]
        public int Id { get; set; }

        [Column("CreditChk_CustID")]
        public int? CustomerId { get; set; }

        [Column("CreditChk_AppID")]
        public string AppId { get; set; }

        [Column("CreditChk_CheckStatus")]
        public string CheckStatus { get; set; }

        [Column("CreditChk_FlagManualNCB")]
        public bool? FlagManualNcb { get; set; }

        [Column("CreditChk_MKTSubmitTime")]
        public DateTime? MarketingSubmitTime { get; set; }

        [Column("CreditChk_CCDocCheckEndTime")]
        public DateTime? CreditDocCheckEndTime { get; set; }

        [Column("CreditChk_StartTime")]
        public DateTime? StartTime { get; set; }

        [Column("CreditChk_EndTime")]
        public DateTime? EndTime { get; set; }

        [Column("CreditChk_CheckBy")]
        public string CheckBy { get; set; }

        [Required]
        [Column("CreditChk_Status")]
        public string Status { get; set; }

        [Column("CreditChk_CreateBy")]
        public string CreateBy { get; set; }

        [Column("CreditChk_CreateDate")]
        public DateTime? CreateDate { get; set; }

        [Column("CreditChk_UpdateBy")]
        public string UpdateBy { get; set; }

        [Column("CreditChk_UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [Column("CreditChk_AutoCheck")]
        public string AutoCheck { get; set; }

        [Column("CreditChk_GenDataMartBy")]
        public string GenDataMartBy { get; set; }

        [Column("CreditChk_GenDataMartDate")]
        public DateTime? GenDataMartDate { get; set; }

        [Column("CreditChk_SMSMessage")]
        public string SmsMessage { get; set; }

        [Column("CreditChk_SMStatus")]
        public string SmStatus { get; set; }

        [Column("CreditChk_SMDetail")]
        public string SmDetail { get; set; }

        [Column("CreditChk_SMID")]
        public string SmId { get; set; }

        [Column("CreditChk_SMTelNo")]
        public string SmTelNo { get; set; }

        [Column("CreditChk_FlagConsentSCRM")]
        public bool? FlagConsentSCRM { get; set; }

        [Column("CreditChk_NCBIsGoodSegment")]
        public string NCBIsGoodSegment { get; set; }

        [Column("CreditChk_MKTIsGoodSegment")]
        public string MarketingIsGoodSegment { get; set; }

        [Column("CreditChk_VERIsGoodSegment")]
        public string VerIsGoodSegment { get; set; }
    }
}
