using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamApi.Models
{
    public class GroupLevel
    {
        [Key]
        [Column("GroupLevel_GroupLevelID")]
        public string Id { get; set; }

        [Column("GroupLevel_ParentID")]
        public string ParentId { get; set; }

        [Column("GroupLevel_BranchID")]
        public string BranchId { get; set; }

        [Column("GroupLevel_GroupLevelName")]
        public string Name { get; set; }

        [Column("GroupLevel_DepartmentID")]
        public string DepartmentId { get; set; }

        [Column("GroupLevel_XPos")]
        public string XPos { get; set; }

        [Column("GroupLevel_YPos")]
        public string YPos { get; set; }

        [Column("GroupLevel_Status")]
        public string Status { get; set; }

        [Column("GroupLevel_ApproveLimitNewCar")]
        public decimal? ApproveLimitNewCar { get; set; }

        [Column("GroupLevel_ApproveLimitOldCar")]
        public decimal? ApproveLimitOldCar { get; set; }

        [Column("GroupLevel_PositionID")]
        public string PositionId { get; set; }
    }
}
