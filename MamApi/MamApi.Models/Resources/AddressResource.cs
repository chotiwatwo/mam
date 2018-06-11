using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class AddressResource
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        //public MktCustomer Customer { get; set; }

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

        public string AddressCode { get; set; }

        public string HouseNo { get; set; }

        public string Floor { get; set; }

        public string RoomNo { get; set; }

        public string Moo { get; set; }

        public string Soi { get; set; }

        public string Road { get; set; }

        public long? DistrictId { get; set; }
        public District District { get; set; }

        public long? AmphurId { get; set; }
        //public Amphur Amphur { get; set; }

        public short? ProvinceId { get; set; }
        //public Province Province { get; set; }

        public string ZipCode { get; set; }

        public string Apartment { get; set; }
    }
}
