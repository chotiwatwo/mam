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
        public string CardTypeDesc { get; set; }

        [Required]
        public string IDCardNo { get; set; }

        [Required]
        public string TitleId { get; set; }
        public string TitleName { get; set; }

        [Required]
        public string FirstNameThai { get; set; }

        [Required]
        public string LastNameThai { get; set; }

        public string Sex { get; set; }
        public string SexDesc { get; set; }

        public DateTime BirthDate { get; set; }

        public MktAddress MailingAddress { get; set; }

        public string LoanType { get; set; }
        public string LoanTypeDesc { get; set; }

        public string NewOrOldCar { get; set; }

        public string PopularBrand { get; set; }
        public string PopularBrandDesc { get; set; }

        public string CarAgeLessThanOrEqual10Years { get; set; }
        public string CarAgeLessThanOrEqual10YearsDesc { get; set; }

        public string GroupOccupationType { get; set; }
        public string GroupOccupationTypeDesc { get; set; }

    }
}
