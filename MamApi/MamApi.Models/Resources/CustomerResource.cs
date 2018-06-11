using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models.Resources
{
    public class CustomerResource
    {
        public int Id { get; set; }

        public string AppId { get; set; }

        public string NewOrOld { get; set; }

        public string TitleId { get; set; }

        public string FirstNameThai { get; set; }

        public string LastNameThai { get; set; }

        public string CardType { get; set; }

        public string IDCardNo { get; set; }

        public ICollection<AddressResource> Addresses { get; set; }

    }
}
