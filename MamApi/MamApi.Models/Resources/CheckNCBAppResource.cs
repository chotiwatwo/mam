using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class CheckNCBAppResource
    {
        public string AppId { get; set; }

        public string NewOrOldCustomer { get; set; }
        
        [Required]
        public string CardType { get; set; }
        //public string CardTypeDesc { get; set; }

        [Required]
        public string IDCardNo { get; set; }

        [Required]
        public string TitleId { get; set; }
        //public string TitleName { get; set; }

        [Required]
        public string FirstNameThai { get; set; }

        [Required]
        public string LastNameThai { get; set; }

        [Required]
        public string SexId { get; set; }
        //public string SexDesc { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        //public MktAddress MailingAddress { get; set; }
        // ที่อยู่จัดส่งเอกสาร
        // รหัสทะเบียนบ้าน
        public string MailingAddressCode { get; set; }

        // บ้านเลขที่
        [Required]
        public string MailingHouseNo { get; set; }

        public string MailingFloor { get; set; }

        public string MailingRoomNo { get; set; }

        public string MailingMoo { get; set; }

        public string MailingSoi { get; set; }

        public string MailingRoad { get; set; }

        [Required]
        public short MailingProvinceId { get; set; }

        [Required]
        public long MailingAmphurId { get; set; }

        [Required]
        public long MailingDistrictId { get; set; }

        [Required]
        public string MailingZipCode { get; set; }

        public string MailingApartment { get; set; }

        public string LoanType { get; set; }
        //public string LoanTypeDesc { get; set; }

        public string NewOrOldCar { get; set; }

        public string PopularBrand { get; set; }
        //public string PopularBrandDesc { get; set; }

        public string CarAgeLessThanOrEqual10Years { get; set; }
        //public string CarAgeLessThanOrEqual10YearsDesc { get; set; }

        public string GroupOccupationType { get; set; }
        //public string GroupOccupationTypeDesc { get; set; }

        public ICollection<AttachmentResource> Attachments { get; set; }
    }
}
