using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamApi.Models
{
    public class Branch
    {
        //  [Branch_BranchID]
        //,[Branch_BranchGroup]
        //,[Branch_BranchName]

        [Key]
        [Column("Branch_BranchID")]
        public string Id { get; set; }

        [Column("Branch_BranchName")]
        public string Name { get; set; }
    }
}
