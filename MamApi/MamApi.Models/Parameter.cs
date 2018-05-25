using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class Parameter
    {
        [Key]
        [Column("Parameter_ParameterID")]
        public string ParameterId { get; set; }

        [Column("Parameter_ParameterValue")]
        public string Value { get; set; }

        [Column("Parameter_Description")]
        protected string Description { get; set; }

        [Column("Parameter_Status")]
        public string Status { get; set; }
    }
}
