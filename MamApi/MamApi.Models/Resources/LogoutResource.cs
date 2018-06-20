using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class LogoutResource
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string IMEI { get; set; }  // หมายเลขอีมี่ ของ Mobile Device 
    }
}
