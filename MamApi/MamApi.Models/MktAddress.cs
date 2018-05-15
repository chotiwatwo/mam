using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class MktAddress
    {
        [Required]
        [Column("MKT_Address_ID")]
        public int Id { get; set; }

        [Required]
        [Column("MKT_Address_CustID")]
        public int CustomerId { get; set; }
        //public MktCustomer Customer { get; set; }

        [Required]
        [Column("MKT_Address_Type")]
        /* 
            Address : ที่อยู่ตามทะเบียนบ้าน
            Current : ที่อยู่ปัจจุบัน
            Office  : ที่อยู่ที่ทำงาน 
            Document : ที่อยู่จัดส่งเอกสาร
            Person :
            Debt :
            Other :
        */
        public string AddressType { get; set; }

        [Column("MKT_Address_Code")]
        public string AddressCode { get; set; }

        [Column("MKT_Address_Address")]
        public string HouseNo { get; set; }

        [Column("MKT_Address_Floor")]
        public string Floor { get; set; }

        [Column("MKT_Address_RoomNo")]
        public string RoomNo { get; set; }

        [Column("MKT_Address_Moo")]
        public string Moo { get; set; }

        [Column("MKT_Address_Soi")]
        public string Soi { get; set; }

        [Column("MKT_Address_Road")]
        public string Road { get; set; }

        [Column("MKT_Address_District")]
        public long? DistrictId { get; set; }
        public District District { get; set; }

        [Column("MKT_Address_Amphur")]
        public long? AmphurId { get; set; }
        //public Amphur Amphur { get; set; }

        [Column("MKT_Address_Province")]
        public short? ProvinceId { get; set; }
        //public Province Province { get; set; }

        [Column("MKT_Address_Zipcode")]
        public string ZipCode { get; set; }

        [Column("MKT_Address_Apartment")]
        public string Apartment { get; set; }
        
        [Column("MKT_Address_AddrMoreInfo")]
        public string MoreInfo { get; set; }

        [Column("MKT_Address_StartYear")]
        public short StartYear { get; set; }

        [Column("MKT_Address_StayYear")]
        public Byte StayYear { get; set; }

        [Column("MKT_Address_NearestPlace")]
        public string NearestPlace { get; set; }

        [Column("MKT_Address_PersonAddrStatus")]
        public string PersonAddrStatus { get; set; }

        [Column("MKT_Address_Map")]
        public string Map { get; set; }

        [Column("MKT_Address_Latitude")]
        public string Latitude { get; set; }

        [Column("MKT_Address_Longtitude")]
        public string Longtitude { get; set; }
        
        [Required]
        [Column("MKT_Address_Status")]
        public string Status { get; set; }

        [Column("MKT_Address_CreateBy")]
        public string CreateBy { get; set; }
        
        [Column("MKT_Address_CreateDate")]
        public DateTime? CreateDate { get; set; }

        [Column("MKT_Address_UpdateBy")]
        public string UpdateBy { get; set; }

        [Column("MKT_Address_UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [Column("MKT_Address_PlaceSendDocument")]
        public string PlaceSendDocument { get; set; }
    }
}
