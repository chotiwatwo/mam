using System;
using System.ComponentModel.DataAnnotations;

namespace MamApi.Models
{
    public class Salesman
    {
        [Key]
        public string MSMN_ID { get; set; } 

        public string MSMN_STATUS { get; set; }
        public string MSMN_TITLE_CODE { get; set; }
        public string MSMN_FIRST_NAME { get; set; }
        public string MSMN_LAST_NAME { get; set; }
        public string MSMN_MARKETING_TYPE { get; set; }
        public string MSMN_BRANCH_GROUP { get; set; }
        public Double? MSMN_BRANCH_CODE { get; set; }
        public Double? MSMN_SUB_BRANCH_CODE { get; set; }
        public string MSMN_MARKETING_SUP { get; set; }

        // To save into HPCS when MKT creates app.
        // 1. MSMN_ID
        // 2. MSMN_BRANCH_GROUP
        // 3. MSMN_MARKETING_SUP
    }
}
