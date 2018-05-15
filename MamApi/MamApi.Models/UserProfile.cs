using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Status { get; set; }

        public bool IsDisable { get; set; }

        public string PositionId { get; set; }
        public string PositionName { get; set; }

        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public string BranchId { get; set; }
        public string BranchName { get; set; }
     
        public string GroupLevelId { get; set; }
        public string GroupLevelName { get; set; }

        public string IMEI { get; set; }
    }
}
