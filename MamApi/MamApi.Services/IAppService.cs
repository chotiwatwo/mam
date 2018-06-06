using MamApi.Models;
using MamApi.Models.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MamApi.Services
{
    public interface IAppService
    {
        IEnumerable<MktApplication> GetApps();

        MktApplication GetApp(string appId);

        MktApplication GetAppToCheckNCB(string appId);

        Task<MktApplication> CreateApp(MktApplication app, UserProfile userProfile);

        MktApplication SaveAppBeforeSubmitToCreditChecking(MktApplication app, UserProfile userProfile);

        void SubmitToCreditChecking(string appId, int customerId, bool hasConsentScoreModel,
            ICollection<AttachmentUploadResource> attachmentUploadResourceFiles,
            UserProfile userProfile);

        //int SaveCreditCheckingForCustomer(string appId, int customerId, bool hasConsentScoreModel,
        //    string createdByUserId);

        //int AddAttachmentFiles(string appId, int customerId, long creditCheckingId,
        //    ICollection<AttachmentUploadResource> attachmentUploadResourceFiles, 
        //    UserProfile userProfile);

        void UpdateApp();

        void RejectApp();

        void CancelApp();

        void Commit();
    }
}
