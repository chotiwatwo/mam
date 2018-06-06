using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamApi.Models
{
    public class MktApplication
    {
        [Key]
        [Column("MKT_Application_ID")]
        public string AppId { get; set; }

        [Column("MKT_Application_BranchID")]
        [StringLength(10)]
        public string BranchId { get; set; }

        //[ForeignKey("MKT_Application_BranchID")]
        public Branch Branch { get; set; }

        //public string MKT_Application_ContractNo { get; set; }
        //public string MKT_Application_ActiveContract_AppID { get; set; }
        //public string MKT_Application_DealerID { get; set; }

        [Required]
        [Column("MKT_Application_AppStatus")]
        public string AppStatus { get; set; }

        [Required]
        [Column("MKT_Application_OwnerID")]
        public string AppOwnerId { get; set; }

        [Required]
        [Column("MKT_Application_Status")]
        public string Status { get; set; }

        //public IList<int> CustomerIds { get; set; }
        
        public MktCustomer Customer { get; set; }

        public MktApplicationExtend ApplicationExtend { get; set; }

        public MktAnnotation Annotation { get; set; }

        public MktLoanType LoanType { get; set; }

        public MktCar Car { get; set; }

        [Column("MKT_Application_CurrentCarID")]
        public long? CurrentCarID { get; set; }

        [Column("MKT_Application_CreateBy")]
        public string CreatedBy { get; set; }

        [Column("MKT_Application_CreateDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("MKT_Application_AppStatus_PreSubmitDate")]
        public DateTime? AppStatusPreSubmitDate { get; set; }

        [Column("MKT_Application_CurrentAppStatus")]
        public string CurrentAppStatus { get; set; }

        [Column("MKT_Application_MKTUsersLatest")]
        public string LatestMarketingUserId { get; set; }

        [Column("MKT_Application_UsersLatest")]
        public string LatestUserId { get; set; }

        [Column("MKT_Application_MKTLastApplogID")]
        public long LastAppLogId { get; set; }


        /*
        public string MktApplicationCimbbranchCode { get; set; }
        public string MktApplicationCampaignId { get; set; }
        public int? MktApplicationNumberGuarantor { get; set; }
        public string MktApplicationCarCheckingId { get; set; }
        public int? MktApplicationIncomeId { get; set; }
        public long? MktApplicationCurrentInvestmentId { get; set; }
        public decimal? MktApplicationSummaryIncome { get; set; }
        public string MktApplicationAppStatus { get; set; }
        public string MktApplicationOwnerId { get; set; }
        public string MktApplicationCcby { get; set; }
        public DateTime? MktApplicationCcdate { get; set; }
        public string MktApplicationCcstatus { get; set; }
        public string MktApplicationStatus { get; set; }
        public string MktApplicationCreateBy { get; set; }
        public DateTime? MktApplicationCreateDate { get; set; }
        public string MktApplicationUpdateBy { get; set; }
        public DateTime? MktApplicationUpdateDate { get; set; }
        public DateTime? MktApplicationAppStatusPreSubmitDate { get; set; }
        public DateTime? MktApplicationAppStatusWaitForSubmitDate { get; set; }
        public DateTime? MktApplicationAppStatusApproveByMktdate { get; set; }
        public DateTime? MktApplicationAppStatusWaitAssignVerDate { get; set; }
        public DateTime? MktApplicationAppStatusVerifiedDate { get; set; }
        public DateTime? MktApplicationAppStatusWaitForApproveVerDate { get; set; }
        public DateTime? MktApplicationAppStatusApproveByVerDate { get; set; }
        public DateTime? MktApplicationAppStatusBookingDate { get; set; }
        public DateTime? MktApplicationAppStatusPreSubmit2Date { get; set; }
        public DateTime? MktApplicationAppStatusWaitForResponseDate { get; set; }
        public DateTime? MktApplicationAppStatusWaitForPreBookingDate { get; set; }
        public DateTime? MktApplicationAppStatusPreBookingDate { get; set; }
        public DateTime? MktApplicationAppStatusRequestConsiderDate { get; set; }
        public DateTime? MktApplicationAppStatusMktcancelDate { get; set; }
        public DateTime? MktApplicationAppStatusMktrejectDate { get; set; }
        public DateTime? MktApplicationAppStatusVercancelCustDate { get; set; }
        public DateTime? MktApplicationAppStatusVerrejectCustDate { get; set; }
        public DateTime? MktApplicationAppStatusVerPreRejectCustDate { get; set; }
        public DateTime? MktApplicationAppStatusTerminateDate { get; set; }
        public DateTime? MktApplicationAppStatusAssignVerDate { get; set; }
        public string MktApplicationCurrentAppStatus { get; set; }
        public DateTime? MktApplicationAppealDate { get; set; }
        public string MktApplicationMktcancelBy { get; set; }
        public string MktApplicationMktrejectBy { get; set; }
        public string MktApplicationMktusersLatest { get; set; }
        public string MktApplicationVerusersLatest { get; set; }
        public string MktApplicationUsersLatest { get; set; }
        public long? MktApplicationMktlastApplogId { get; set; }
        public long? MktApplicationVerlastApplogId { get; set; }
        public string MktApplicationFastTrack { get; set; }
        */

    }
}
