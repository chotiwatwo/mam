using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Data.Repositories;
using MamApi.Models;

namespace MamApi.Services
{
    public class AppService : IAppService
    {
        private readonly IAppRepository _appRepo;
        private readonly IMasterService _masterService;

        public AppService(IAppRepository appRepo, IMasterService masterService)
        {
            _appRepo = appRepo;
            _masterService = masterService;
        }

        public void CancelApp()
        {
            
        }

        public void Commit()
        {

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

        public MktApplication GetApp(string appId)
        {
            var app = _appRepo
                        .FindByInclude(
                            a => a.AppId == appId,
                            a => a.ApplicationExtend,
                            a => a.Annotation,
                            a => a.Customer)
                        .FirstOrDefault();

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

        public void RejectApp()
        {

        }

        public void UpdateApp()
        {

        }

        private string GetCustomerSexFromTitle(string titleId) {
            var _customerSex = _masterService
                                 .GetMasterInfosByIdAndType(titleId, "Title", "A")
                                 .Select(m => m.MappingValidate)
                                 .SingleOrDefault();

            return _customerSex;
        }
    }
}
