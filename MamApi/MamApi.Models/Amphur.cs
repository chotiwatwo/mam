using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class Amphur
    {
        [Required]
        [Column("Amphur_AmphurID")]
        public long Id { get; set; }

        [Required]
        [Column("Amphur_ProvinceID")]
        public short ProvinceId { get; set; }
        public Province Province { get; set; }

        [Required]
        [Column("Amphur_AmphurName")]
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }

        [Required]
        [Column("Amphur_ZipCode")]
        public string ZipCode { get; set; }

        [Required]
        [Column("Amphur_Status")]
        public string Status { get; set; }

        //[Column("Amphur_CreateBy")]
        //public string CreateBy { get; set; }

        //[Column("Amphur_CreateDate")]
        //public DateTime? CreateDate { get; set; }

        //[Column("Amphur_UpdateBy")]
        //public string UpdateBy { get; set; }

        //[Column("Amphur_UpdateDate")]
        //public DateTime? UpdateDate { get; set; }

        //[Column("Amphur_mappingHPPro")]
        //public string MappingHppro { get; set; }

        //[Column("Amphur_PrvMappingHPPro")]
        //public string PrvMappingHppro { get; set; }
    }
}
