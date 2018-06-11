using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamApi.Models
{
    public class CcConsent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Consent_ID")]
        public long Id { get; set; }

        [Required]
        [Column("Consent_CCCreditChkID")]
        public int CcCreditChkId { get; set; }

        [Column("Consent_Type")]
        public string ConsentType { get; set; }

        [Column("Consent_ReceiveBy")]
        public string ReceiveBy { get; set; }

        [Column("Consent_ReceiveDate")]
        public DateTime? ReceiveDate { get; set; }

        [Column("Consent_ReceiveStatus")]
        public string ReceiveStatus { get; set; }

        [Column("Consent_ReceivePeriod")]
        public int? ReceivePeriod { get; set; }

        [Column("Consent_Remark")]
        public string Remark { get; set; }

        [Required]
        [Column("Consent_Status")]
        public string Status { get; set; }

        [Column("Consent_CreateBy")]
        public string CreateBy { get; set; }

        [Column("Consent_CreateDate")]
        public DateTime? CreateDate { get; set; }

        [Column("Consent_UpdateBy")]
        public string UpdateBy { get; set; }

        [Column("Consent_UpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
