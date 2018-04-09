using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class UserProfileResource
    {      
        public long Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Status { get; set; }

        public bool IsDisable { get; set; }

        public Position Position { get; set; }

        public Department Department { get; set; }

        public Branch Branch { get; set; }
    }
}
