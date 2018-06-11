using System;

namespace MamApi.Models
{
    public class MessageInfo
    {
        public string Title { get; set; }
        public string AppId { get; set; }
        public string Body { get; set; }
        public string SenderDepartment { get; set; }
        public string SenderUsername { get; set; }
        public DateTime? SentTime { get; set; }

        public string ActionName { get; set; }
    }
}
