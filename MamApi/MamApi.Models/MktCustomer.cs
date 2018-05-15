using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class MktCustomer
    {
        [Key]
        [Column("Customer_ID")]
        public int Id { get; set; }

        [Required]
        [Column("Customer_AppID")]
        public string AppId { get; set; }

        [Column("Customer_AppCustType")]
        public string AppCustomerType { get; set; }

        [Column("Customer_NewOldType")]
        public string NewOrOld { get; set; }

        [Column("Customer_Type")]
        public string CustomerType { get; set; }

        [Column("Customer_Prefix")]
        public string TitleId { get; set; }

        [Column("Customer_FNameTH")]
        public string FirstNameThai { get; set; }

        [Column("Customer_MNameTH")]
        public string MiddleNameThai { get; set; }

        [Column("Customer_LNameTH")]
        public string LastNameThai { get; set; }

        [Column("Customer_Sex")]
        public string Sex { get; set; }

        [Column("Customer_CardType")]
        public string CardType { get; set; }

        [Required]
        [Column("Customer_IDCardNo")]
        public string IDCardNo { get; set; }

        [Required]
        [Column("Customer_Status")]
        public string Status { get; set; }

        public ICollection<MktAddress> Addresses { get; set; }

        [Column("Customer_CreateBy")]
        public string CreatedBy { get; set; }

        [Column("Customer_CreateDate")]
        public DateTime? CreatedDate { get; set; }

        public MktAsset Asset { get; set; }
    }
}
