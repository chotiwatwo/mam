using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamApi.Models
{
    public class MktLoanType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MKT_LoanType_ID")]
        public long Id { get; set; }

        [Required]
        [Column("MKT_LoanType_AppID")]
        public string AppId { get; set; }

        [Column("MKT_LoanType_TypeID")]
        public string TypeId { get; set; }

        [Column("MKT_LoanType_ValueID")]
        public string ValueId { get; set; }

        [Required]
        [Column("MKT_LoanType_Status")]
        public string Status { get; set; }

        [Column("MKT_LoanType_CreateBy")]
        public string CreateBy { get; set; }

        [Column("MKT_LoanType_CreateDate")]
        public DateTime? CreateDate { get; set; }

        [Column("MKT_LoanType_UpdateBy")]
        public string UpdateBy { get; set; }

        [Column("MKT_LoanType_UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [Column("MKT_LoanType_PopularBrand")]
        public string PopularBrand { get; set; }

        [Column("MKT_LoanType_IsCarlessandequal10")]
        public string IsCarlessOrEqual10 { get; set; }

        [Column("MKT_LoanType_GroupOccType")]
        public string GroupOccType { get; set; }
    }
}
