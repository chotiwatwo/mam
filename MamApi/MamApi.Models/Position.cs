using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class Position
    {
        [Key]
        [Column("Position_PositionID")]
        public string Id { get; set; }

        [Column("Position_PositionName")]
        public string Name { get; set; }

    }
}
