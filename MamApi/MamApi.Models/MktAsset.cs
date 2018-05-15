using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class MktAsset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Asset_ID")]
        public long Id { get; set; }

        [Required]
        [Column("Asset_CustomerID")]
        public int CustomerId { get; set; }

        [Required]
        [Column("Asset_Is_Have")]
        public bool IsHave { get; set; }

        [Column("Asset_LandSize_Rai")]
        public short? LandSizeRai { get; set; }

        [Column("Asset_LandSize_Wa")]
        public short? LandSizeWa { get; set; }

        [Column("Asset_LandAmount")]
        public decimal? LandAmount { get; set; }

        [Column("Asset_HouseSize_Meter")]
        public int? HouseSizeMeter { get; set; }

        [Column("Asset_HouseAmount")]
        public decimal? HouseAmount { get; set; }

        [Column("Asset_TotalAmount")]
        public decimal? TotalAmount { get; set; }

        [Column("Asset_LivingStatus")]
        public string LivingStatus { get; set; }

        [Column("Asset_CurrentAddrStatus")]
        public string CurrentAddrStatus { get; set; }

        [Column("Asset_EnvironmentStatus")]
        public string EnvironmentStatus { get; set; }

        [Column("Asset_HouseOwnership")]
        public string HouseOwnership { get; set; }

        [Column("Asset_LandOwnership")]
        public string LandOwnership { get; set; }

        [Column("Asset_HouseType")]
        public string HouseType { get; set; }

        [Column("Asset_HouseFloor")]
        public string HouseFloor { get; set; }

        [Column("Asset_HouseUnit")]
        public string HouseUnit { get; set; }

        [Column("Asset_PropertyPerDebt")]
        public string PropertyPerDebt { get; set; }

        [Column("Asset_ConditionInHouse")]
        public string ConditionInHouse { get; set; }

        [Required]
        [Column("Asset_Status")]
        public string Status { get; set; }

        [Column("Asset_CreateBy")]
        public string CreateBy { get; set; }

        [Column("Asset_CreateDate")]
        public DateTime? CreateDate { get; set; }

        [Column("Asset_UpdateBy")]
        public string UpdateBy { get; set; }

        [Column("Asset_UpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
