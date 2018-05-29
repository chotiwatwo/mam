using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamApi.Models
{
    public class MktCar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Car_ID")]
        public long Id { get; set; }

        [Required]
        [Column("Car_AppID")]
        public string AppId { get; set; }

        [Required]
        [Column("Car_OldNewCar")]
        public string OldNewCar { get; set; }

        [Required]
        [Column("Car_OldCarVatType")]
        public string OldCarVatType { get; set; }

        [Column("Car_OwnDate")]
        public DateTime? OwnDate { get; set; }

        [Column("Car_LicenseDate")]
        public DateTime? LicenseDate { get; set; }

        [Required]
        [Column("Car_LicenseNo")]
        public string LicenseNo { get; set; }

        [Column("Car_RegisterType")]
        public string RegisterType { get; set; }

        [Column("Car_RegisterProvince")]
        public string RegisterProvince { get; set; }

        [Column("Car_CarType")]
        public string Type { get; set; }

        [Column("Car_CarSubType")]
        public string SubType { get; set; }

        [Column("Car_BrandCode")]
        public string BrandCode { get; set; }

        [Column("Car_Model")]
        public string Model { get; set; }

        [Column("Car_Series")]
        public string Series { get; set; }

        [Column("Car_GearType")]
        public string GearType { get; set; }

        [Column("Car_ModelYear")]
        public string ModelYear { get; set; }

        [Column("Car_ModelMonth")]
        public string ModelMonth { get; set; }

        [Column("Car_ColorCode")]
        public string ColorCode { get; set; }

        [Required]
        [Column("Car_ChassisCode")]
        public string ChassisCode { get; set; }

        [Column("Car_ChassisPosition")]
        public string ChassisPosition { get; set; }

        [Column("Car_EngineCode")]
        public string EngineCode { get; set; }

        [Column("Car_EnginePosition")]
        public string EnginePosition { get; set; }

        [Column("Car_EngineBrand")]
        public string EngineBrand { get; set; }

        [Column("Car_FuelType")]
        public string FuelType { get; set; }

        [Column("Car_GasCode")]
        public string GasCode { get; set; }

        [Column("Car_InjectionNumber")]
        public string InjectionNumber { get; set; }

        [Column("Car_EngineSize")]
        public string EngineSize { get; set; }

        [Column("Car_HorsePower")]
        public string HorsePower { get; set; }

        [Column("Car_Distance")]
        public string Distance { get; set; }

        [Column("Car_Weight")]
        public string Weight { get; set; }

        [Column("Car_WeightLoad")]
        public string WeightLoad { get; set; }

        [Column("Car_TotalWeight")]
        public string TotalWeight { get; set; }

        [Column("Car_SeatNumber")]
        public string SeatNumber { get; set; }

        [Column("Car_Grade")]
        public string Grade { get; set; }

        [Required]
        [Column("Car_HirePrice")]
        public decimal HirePrice { get; set; }

        [Column("Car_RefPrice")]
        public decimal? RefPrice { get; set; }

        [Column("Car_NewCarPrice")]
        public decimal? NewCarPrice { get; set; }

        [Column("Car_AssessmentPrice")]
        public decimal? AssessmentPrice { get; set; }

        [Column("Car_UsedArea")]
        public string UsedArea { get; set; }

        [Column("Car_UsedBehavior")]
        public string UsedBehavior { get; set; }

        [Column("Car_OwerType")]
        public string OwerType { get; set; }

        [Column("Car_ParkPlace")]
        public string ParkPlace { get; set; }

        [Required]
        [Column("Car_Status")]
        public string Status { get; set; }

        [Column("Car_CreateBy")]
        public string CreateBy { get; set; }

        [Column("Car_CreateDate")]
        public DateTime? CreateDate { get; set; }

        [Column("Car_UpdateBy")]
        public string UpdateBy { get; set; }

        [Column("Car_UpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
