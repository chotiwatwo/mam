using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class Message
    {
        //public string Token { get; set; }
        public string to { get; set; }
        public MessageInfo data { get; set; }
    }
}
