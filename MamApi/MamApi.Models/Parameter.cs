using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class Parameter
    {
        public string ParameterId { get; set; }

        public string Value { get; set; }

        protected string Description { get; set; }

        public string Status { get; set; }
    }
}
