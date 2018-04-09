using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class Department
    {
        [Key]
        [Column("Department_DepartmentID")]
        public string Id { get; set; }

        [Column("Department_DepartmentName")]
        public string Name { get; set; }

    }
}
