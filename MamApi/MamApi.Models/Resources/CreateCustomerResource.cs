using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class CreateCustomerResource
    {
        [Key]
        public int Id { get; set; }

        public string AppId { get; set; }

        public string NewOrOld { get; set; }

        public string TitleId { get; set; }

        public string FirstNameThai { get; set; }

        public string LastNameThai { get; set; }

        public string CardType { get; set; }

        public string IDCardNo { get; set; }
    }
}
