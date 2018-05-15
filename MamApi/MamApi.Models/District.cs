using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class District
    {
        [Required]
        [Column("District_DistrictID")]
        public long Id { get; set; }

        [Required]
        [Column("District_AmphurID")]
        public long AmphurId { get; set; }
        public Amphur Amphur { get; set; }

        [Required]
        [Column("District_DistrictName")]
        public string Name { get; set; }

        [Required]
        [Column("District_Status")]
        public string Status { get; set; }

        //[Column("District_CreateBy")]
        //public string CreateBy { get; set; }

        //[Column("District_CreateDate")]
        //public DateTime? CreateDate { get; set; }

        //[Column("District_UpdateBy")]
        //public string UpdateBy { get; set; }

        //[Column("District_UpdateDate")]
        //public DateTime? UpdateDate { get; set; }

        //[Column("District_MappingHPPro")]
        //public string MappingHppro { get; set; }

        //[Column("District_AmpMappingHPPro")]
        //public string AmpMappingHppro { get; set; }

        //[Column("District_PrvMappingHPPro")]
        //public string PrvMappingHppro { get; set; }
    }
}
