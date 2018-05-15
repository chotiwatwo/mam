using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Models
{
    public class MasterInfo
    {
        // Primary Key : InfoId + InfoType
        [Column("MasterInfo_MasterInfoID")]
        public string Id { get; set; }
        
        [Column("MasterInfo_MasterInfoType")]
        public string Type { get; set; }

        [Column("MasterInfo_MasterInfoEName")]
        public string EnglishName { get; set; }

        [Column("MasterInfo_MasterInfoTName")]
        public string ThaiName { get; set; }

        [Column("MasterInfo_MasterInfoDescription")]
        public string Description { get; set; }

        [Column("MasterInfo_Status")]
        public string Status { get; set; }

        [Column("MasterInfo_MappingValidate")]
        public string MappingValidate { get; set; } // ใช้เฉพาะ type "Title" เพื่อ map คำนำหน้า กับ เพศ

        //  [MasterInfo_MasterInfoID]
        //,[MasterInfo_MasterInfoType]
        //,[MasterInfo_MasterInfoEName]
        //,[MasterInfo_MasterInfoTName]
        //,[MasterInfo_MasterInfoDescription]
        //,[MasterInfo_Status]
        //,[MasterInfo_CreateBy]
        //,[MasterInfo_CreateDate]
        //,[MasterInfo_UpdateBy]
        //,[MasterInfo_UpdateDate]
        //,[MasterInfo_MappingAHP]
        //,[MasterInfo_MappingSCRM]
        //,[MasterInfo_MappingValidate]
        //,[MasterInfo_MappingInsurance]
        //,[MasterInfo_MappingHPPro]
        //,[MasterInfo_MasterInfoValue]
    }
}
