using System.ComponentModel.DataAnnotations;

namespace MamApi.Models.Resources
{
    public class LoginResource
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        //[MinLength(15)]
        //[MaxLength(15)]
        public string IMEI { get; set; }  // หมายเลขอีมี่ ของ Mobile Device 
    }
}
