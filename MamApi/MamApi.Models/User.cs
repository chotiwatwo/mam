using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class User
    {
        [Key]
        [Column("User_ID")]
        public long Id { get; set; }

        [Column("User_UserID")]
        public string UserId { get; set; }

        [Column("User_UserName")]
        public string UserName { get; set; }

        [Column("User_Pwd")]
        public string Password { get; set; }

        [Column("User_Status")]
        public string Status { get; set; }
        
        [Column("User_Disable")]
        public bool IsDisable { get; set; }

        [Column("User_PositionID")]
        public string PositionID { get; set; }

        public Position Position { get; set; }

        [Column("User_DepartmentID")]
        public string DepartmentId { get; set; }

        public Department Department { get; set; }

        [Column("User_BranchID")]
        public string BranchId { get; set; }

        public Branch Branch { get; set; }

        


    }
}
