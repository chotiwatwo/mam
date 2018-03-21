using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MamApi.Models
{
    public class Branch
    {
        //  [Branch_BranchID]
        //,[Branch_BranchGroup]
        //,[Branch_BranchName]

        [Key]
        [StringLength(10)]
        public string Branch_BranchID { get; set; }

        public string Branch_BranchName { get; set; }
    }
}
