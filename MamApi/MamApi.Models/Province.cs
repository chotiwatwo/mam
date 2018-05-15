using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class Province
    {
        [Key]
        [Column("Province_ProvinceID")]
        public short Id { get; set; }

        [Column("Province_ProvinceName")]
        public string Name { get; set; }

        [Column("Province_Status")]
        public string Status { get; set; }

        public ICollection<Amphur> Amphurs { get; set; }

        //[Column("Province_CreateBy")]
        //public string CreateBy { get; set; }

        //[Column("Province_CreateDate")]
        //public DateTime? CreateDate { get; set; }

        //[Column("Province_UpdateBy")]
        //public string UpdateBy { get; set; }

        //[Column("Province_UpdateDate")]
        //public DateTime? UpdateDate { get; set; }

        //[Column("Province_MappingAHP")]
        //public string MappingAhp { get; set; }

        //[Column("Province_MappingSCRM")]
        //public string MappingScrm { get; set; }

        //[Column("Province_MappingHPPro")]
        //public string MappingHppro { get; set; }
    }
}
