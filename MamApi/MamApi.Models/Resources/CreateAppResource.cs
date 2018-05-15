using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class CreateAppResource
    {
        public string AppId { get; set; }

        [Required]
        public string CardType { get; set; }

        [Required]
        public string IDCardNo { get; set; }

        [Required]
        public string TitleId { get; set; }

        [Required]
        public string FirstNameThai { get; set; }

        [Required]
        public string LastNameThai { get; set; }

        //public CreateCustomerResource Customer { get; set; }

        //public string BranchId { get; set; }

        //public string AppStatus { get; set; }

        //public string AppOwnerId { get; set; }

        //public string Status { get; set; }

        //public string CreatedBy { get; set; }

        //public DateTime CreatedDate { get; set; }

        //public DateTime AppStatusPreSubmitDate { get; set; }

        //public string CurrentAppStatus { get; set; }

        //public string LatestMarketingUserId { get; set; }

        //public string LatestUserId { get; set; }

        //public long AppLogId { get; set; }
    }
}
