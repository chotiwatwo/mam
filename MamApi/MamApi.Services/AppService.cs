using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Data.Repositories;
using MamApi.Models;
using MamApi.Models.Resources;

namespace MamApi.Services
{
    public class AppService : IAppService
    {
        private readonly IAppRepository _appRepo;
        private readonly IMasterService _masterService;
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly ICreditCheckingRepository _creditCheckingRepo;

        public AppService(IAppRepository appRepo, IMasterService masterService, 
            IAttachmentRepository attachmentRepo, 
            ICreditCheckingRepository creditCheckingRepo)
        {
            _appRepo = appRepo;
            _masterService = masterService;
            _attachmentRepo = attachmentRepo;
            _creditCheckingRepo = creditCheckingRepo;
        }

        private string GetCustomerSexFromTitle(string titleId)
        {
            var _customerSex = _masterService
                                 .GetMasterInfosByIdAndType(titleId, "Title", "A")
                                 .Select(m => m.MappingValidate)
                                 .SingleOrDefault();

            return _customerSex;
        }

        public async Task<MktApplication> CreateApp(MktApplication app, UserProfile userProfile)
        {
            using (var transaction = _appRepo.BeginTransaction())
            {
                string appMaxId = _appRepo.GetMaxApplicationIdByUserId(app.AppOwnerId, transaction); // ST12511

                app.AppId = appMaxId;
                app.BranchId = appMaxId.Substring(0, 2);
                app.AppStatus = BusinessConstant.AppStatusMarketingInitial;
                app.Status = BusinessConstant.StatusActive;
                app.CreatedBy = app.AppOwnerId;
                app.CreatedDate = DateTime.Now;
                app.AppStatusPreSubmitDate = DateTime.Now;
                app.CurrentAppStatus = app.AppStatus;
                app.LatestMarketingUserId = app.AppOwnerId;
                app.LatestUserId = app.AppOwnerId;

                //app.LastAppLogId = 0;

                app.Customer.Id = _appRepo.GetMaxCustomerId(transaction);
                app.Customer.Status = BusinessConstant.StatusActive;
                app.Customer.AppId = appMaxId;
                app.Customer.MiddleNameThai = string.Empty;
                app.Customer.AppCustomerType = BusinessConstant.CustomerAppCusTypePersonal;
                app.Customer.CustomerType = BusinessConstant.CustomerTypePurchase;
                app.Customer.CreatedBy = app.AppOwnerId;
                app.Customer.CreatedDate = DateTime.Now;

                app.Customer.Sex = GetCustomerSexFromTitle(app.Customer.TitleId);

                MktAsset asset = new MktAsset {
                    CustomerId = app.Customer.Id,
                    IsHave = true,
                    LandSizeRai = 0,
                    LandSizeWa = 0,
                    LandAmount = 0,
                    HouseSizeMeter = 0,
                    HouseAmount = 0,
                    TotalAmount = 0,
                    Status = BusinessConstant.StatusActive,
                    CreateBy = app.CreatedBy,
                    CreateDate = DateTime.Now
                };

                app.Customer.Asset = asset;

                app.ApplicationExtend.AppId = app.AppId;
                app.ApplicationExtend.OwnerBranchId = app.BranchId;

                app.Annotation = new MktAnnotation {
                    AppId = app.AppId,
                    ApproveFlag = BusinessConstant.FlagYes,
                    ApproveRemark = string.Empty,
                    Status = BusinessConstant.StatusActive,
                    CreateBy = app.AppOwnerId,
                    CreateDate = DateTime.Now,
                    UpdateBy = app.AppOwnerId,
                    UpdateDate = DateTime.Now
                };

                ApplicationLog appLog = new ApplicationLog {
                    AppId = app.AppId,

                    FromUserId = userProfile.UserId,
                    FromLevelId = userProfile.GroupLevelId,
                    FromDepartmentID = userProfile.DepartmentId,
                    FromBranchID = userProfile.BranchId,

                    AppLogDate = DateTime.Now,
                    AppStatus = BusinessConstant.AppStatusMarketingInitial,

                    Remark = "Create App from MAM",
                    ActionName = "Create App",
                    Status = BusinessConstant.StatusActive,

                    CreateBy = userProfile.UserId,
                    CreateDate = DateTime.Now
                };

                _appRepo.InsertApplicationLog(appLog, transaction);

                app.LastAppLogId = appLog.AppLogId;

                _appRepo.Add(app);

                await _appRepo.CommitAsync();

                transaction.Commit();
            }

            return app;
        }

        public MktApplication SaveAppBeforeSubmitToCreditChecking(MktApplication app, UserProfile userProfile)
        {
            using (var transaction = _appRepo.BeginTransaction())
            {
                _appRepo.Commit();

                _appRepo.UpdateApplicationCurrentCarId(app.Car.Id, app.AppId, transaction);

                // FastTrack เป็น 0 เพราะยังไม่ได้เลือก [อาชีพ]
                _appRepo.UpdateFastTrack(app.AppId, transaction);

                transaction.Commit();
            }

            return app;
        }

        public void SubmitToCreditChecking(string appId, int customerId, bool hasConsentScoreModel,
            ICollection<AttachmentUploadResource> attachmentUploadResourceFiles,
            UserProfile userProfile)
        {
            using (var transaction = _creditCheckingRepo.BeginTransaction())
            {
                CcCreditChk creditChecking = SaveCreditCheckingForCustomer(appId, customerId,
                    hasConsentScoreModel, userProfile.UserId);

                _creditCheckingRepo.Commit();

                int creditCheckingId = creditChecking.Id;

                int attachmentFileCount = AddAttachmentFiles(appId, customerId,
                    creditCheckingId, attachmentUploadResourceFiles, userProfile.UserId);

                _attachmentRepo.Commit();
                
                _creditCheckingRepo.SaveConsentReceiveStatus(creditCheckingId, userProfile.UserId);

                _creditCheckingRepo.Commit();

                transaction.Commit();
            }

        }

        private CcCreditChk SaveCreditCheckingForCustomer(string appId, int customerId, bool hasConsentScoreModel,
            string createdByUserId)
        {
            var creditCheckingFromRepo = _creditCheckingRepo
                .Query(c => c.AppId == appId && c.CustomerId == customerId)
                .SingleOrDefault();

            if (creditCheckingFromRepo == null)
            {
                creditCheckingFromRepo = new CcCreditChk
                {
                    CustomerId = customerId,
                    AppId = appId,
                    CheckStatus = BusinessConstant.CreditCheckingStatusOnProcess,
                    FlagManualNcb = false,
                    MarketingSubmitTime = DateTime.Now,
                    Status = BusinessConstant.StatusActive,
                    CreateBy = createdByUserId,
                    CreateDate = DateTime.Now,
                    FlagConsentSCRM = hasConsentScoreModel
                };
            }

            _creditCheckingRepo.Add(creditCheckingFromRepo);
            //_creditCheckingRepo.Commit();

            return creditCheckingFromRepo;
        }

        private int AddAttachmentFiles(string appId, int customerId, long creditCheckingId,
            ICollection<AttachmentUploadResource> attachmentUploadResourceFiles,
            string createdByUserId)
        {
            // Upsert Attachment rows for this App. and Customer
            IEnumerable<Attachment> attachmentsFromRepo = _attachmentRepo.Query(a => a.AppId == appId && a.CustomerId == customerId);

            int seq = 1;

            if (attachmentsFromRepo != null) 
            {
                // Loop by New Upload files from User
                foreach (var newAttachment in attachmentUploadResourceFiles)
                {
                    var existingAttachment = _attachmentRepo.FindByKey(seq, newAttachment.AppId, 
                                                newAttachment.CustomerId);

                    // Add new if not found
                    if (existingAttachment == null)
                    {
                        _attachmentRepo.Add(new Attachment
                        {
                            Id = seq,
                            AppId = newAttachment.AppId,
                            CustomerId = newAttachment.CustomerId,
                            CCCreditChkId = creditCheckingId,
                            AttachmentType = newAttachment.AttachmentType,
                            Name = newAttachment.Name,
                            FileName = string.Empty,
                            Remark = string.Empty,
                            Status = BusinessConstant.StatusActive,
                            CreateBy = createdByUserId,
                            CreateDate = DateTime.Now,
                            UpdateBy = createdByUserId,
                            UpdateDate = DateTime.Now
                        });
                    }
                    else
                    {
                        existingAttachment.Id = seq;
                        existingAttachment.AppId = newAttachment.AppId;
                        existingAttachment.CustomerId = newAttachment.CustomerId;
                        existingAttachment.CCCreditChkId = creditCheckingId;
                        existingAttachment.AttachmentType = newAttachment.AttachmentType;
                        existingAttachment.Name = newAttachment.Name;
                        existingAttachment.FileName = string.Empty;
                        existingAttachment.Remark = string.Empty;
                        existingAttachment.Status = BusinessConstant.StatusActive;
                        existingAttachment.CreateBy = createdByUserId;
                        existingAttachment.CreateDate = DateTime.Now;
                        existingAttachment.UpdateBy = createdByUserId;
                        existingAttachment.UpdateDate = DateTime.Now;
                    }


                    //    //drSubAttachment.Attachment_CCCreditChkID = this.CCCreditChk_ID;

                    seq++;
                }

                //_attachmentRepo.Commit();
            }

            return seq;
        }

        public MktApplication GetApp(string appId)
        {
            //var app = _appRepo
            //            .FindByInclude(
            //                a => a.AppId == appId,
            //                a => a.ApplicationExtend,
            //                a => a.Annotation,
            //                a => a.Customer)
            //            .FirstOrDefault();

            var app = _appRepo.GetFullApp(appId);

            return app;
        }

        public MktApplication GetAppToCheckNCB(string appId)
        {
            var appToCheckNCB = _appRepo.GetShortApp(appId);

            return appToCheckNCB;
        }

        public IEnumerable<MktApplication> GetApps()
        {
            var apps = _appRepo.FindByInclude(
                            a => a.AppId.StartsWith("0161"), 
                            b => b.Branch);

            return apps;
        }

        public void UpdateApp()
        {

        }

        public void RejectApp()
        {

        }

        public void CancelApp()
        {

        }

        public void Commit()
        {
            _appRepo.Commit();
        }

        
    }
}
