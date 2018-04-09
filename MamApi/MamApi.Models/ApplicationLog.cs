using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class ApplicationLog
    {
        [Key]
        public long AppLogId { get; set; }

        public string AppId { get; set; }

        public string FromUserId { get; set; }

        public string FromLevelId { get; set; }
    }
}
