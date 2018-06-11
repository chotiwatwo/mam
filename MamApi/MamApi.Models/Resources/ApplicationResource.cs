using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class ApplicationResource
    {
        #region <<< Application Header>>>
        public string AppId { get; set; }

        //public string CardType { get; set; }

        //public string IDCardNo { get; set; }

        //public string TitleId { get; set; }
        //public string TitleName { get; set; }

        //public string FirstNameThai { get; set; }

        //public string LastNameThai { get; set; }

        public string AppOwnerId { get; set; }
        public User AppOwner { get; set; }
        //public string MarketingOwnerName { get; set; }

        //public string BranchName { get; set; }
        public string BranchId { get; set; }
        public Branch Branch { get; set; }

        public string MarketingAQT { get; set; }

        #endregion Application Header

        #region <<<Application Detail>>>
        public CustomerResource Customer { get; set; }

        #endregion Application Detail
    }
}
