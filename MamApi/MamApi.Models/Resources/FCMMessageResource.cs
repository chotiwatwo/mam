using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class FCMMessageResource
    {
        public string to { get; set; }
        public NotificationInfo notification { get; set; }
        public MessageInfo data { get; set; }

        //public Message message { get; set; }
    }
}
