using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MamApi.Services;
using MamApi.Models.Resources;
using AutoMapper;
using MamApi.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using Serilog;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MamApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Apps")]
    public class AppsController : Controller
    {
        private readonly IAppService _appService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AppsController(IAppService appService, IAuthService authService, IMapper mapper)
        {
            _appService = appService;
            _authService = authService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult test() {
            return Ok("Yes");
        }

        [HttpGet]
        public IActionResult GetApps() {
            var apps = _appService.GetApps();

            return Ok(apps);
        }

        //[AllowAnonymous]
        [HttpGet("{appId}", Name = "GetApp")]
        public IActionResult GetApp(string appId, bool toCheckNCB = false)
        {
            try
            {
                Log.Information("This is {@appId}", appId);
                MktApplication app;

                if (!toCheckNCB)
                {
                    app = _appService.GetApp(appId);
                }
                else
                {
                    app = _appService.GetAppToCheckNCB(appId);
                }

                if (app == null)
                {
                    //return NotFound($"Application No : [{ appNo }] was not found");
                    return NotFound($"ไม่พบเลขที่ใบคำขอ : [{ appId }] ในระบบ");
                }

                if (!toCheckNCB)
                {
                    var appResource = _mapper.Map<MktApplication, ApplicationResource>(app);

                    return Ok(appResource);
                }
                else
                {
                    var checkNCBAppResource = _mapper.Map<MktApplication, CheckNCBAppResource>(app);

                    return Ok(checkNCBAppResource);
                }

                //return Ok(new { checkNcbApp = checkNCBAppResource, fullApp = app });
            }
            catch (Exception ex)
            {
                Log.Error("มี error {@Except}", ex);

                return BadRequest(new ErrorMessage { ErrorText = ex.Message });
            }

        }

        /* 
            FromBody (json) => CreateAppResource
            {
                "AppId": "",
                "CardType": "30",
                "IDCardNo": "3101501514494",
                "TitleId": "A1",
                "FirstNameThai": "โชติวัติ",
                "LastNameThai": "วงศ์ถา"
            }
        */
        [HttpPost]
        public async Task<IActionResult> CreateApp([FromBody] CreateAppResource createAppResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //string branchId = await _authService.GetBranchIdFromUserProfile(HttpContext);
                //string userId = await _authService.GetUserIdFromUserProfile(HttpContext);

                Log.Information("From Body {@CreateAppResource}", createAppResource);
                
                UserProfile userProfile = await _authService.GetUserProfileFromToken(HttpContext);

                MktCustomer customer = new MktCustomer()
                {
                    CardType = createAppResource.CardType,
                    IDCardNo = createAppResource.IDCardNo,
                    NewOrOld = BusinessConstant.CustomerNewType,
                    TitleId = createAppResource.TitleId,
                    FirstNameThai = createAppResource.FirstNameThai,
                    LastNameThai = createAppResource.LastNameThai
                };

                MktApplicationExtend appExtend = new MktApplicationExtend()
                {
                    CurrentBy = userProfile.UserId,
                    CurrentLevel = userProfile.GroupLevelId
                };

                MktApplication app = new MktApplication() {
                    AppOwnerId = userProfile.UserId,

                    ApplicationExtend = appExtend,
                    Customer = customer
                    
                };

                var createdApp = await _appService.CreateApp(app, userProfile);

                string newURI = Url.Link("GetApp", new { appId = createdApp.AppId, toCheckNCB = true });

                var resultApp = new { appId = createdApp.AppId, url = newURI };

                return Created(newURI, resultApp);
            }
            catch (Exception ex)
            {
                Log.Error("This is {@Exception}", ex);
            }

            return BadRequest();
        }

        private string ValidateInputForCreditChecking(CheckNCBAppResource checkNCBApp)
        {
            #region ### Validate Attach Document Files Required ###
            // 1. Validate Attach Document Files 
            // ต้องมี ประเภทเอกสาร (C, I, F) และ (O หรือ T) ครบอย่างน้อย 4 ประเภท
            List<AttachmentUploadResource> attachmentFiles = (List<AttachmentUploadResource>)checkNCBApp.Attachments;

            bool HasConsent = attachmentFiles
                              .Exists(f => f.AttachmentType == BusinessConstant.AttachmentTypeConsent);

            bool HasIndividual = attachmentFiles
                                .Exists(f => f.AttachmentType == BusinessConstant.AttachmentTypeIndividual);

            bool HasFrontApp = attachmentFiles
                               .Exists(f => f.AttachmentType == BusinessConstant.AttachmentTypeFrontApplicationForm);

            bool HasConsentModelOrMemoInternal =
                (attachmentFiles.Exists(f => f.AttachmentType == BusinessConstant.AttachmentTypeConsentModel) ||
                 attachmentFiles.Exists(f => f.AttachmentType == BusinessConstant.AttachmentTypeMemoInternal));

            if (!(HasConsent && HasIndividual && HasFrontApp && HasConsentModelOrMemoInternal))
            {
                StringBuilder validateMsg = new StringBuilder();

                validateMsg.Append(@"จะต้องมี ประเภทเอกสาร ");
                validateMsg.Append(@"C = Consent, ");
                validateMsg.Append(@"I = บัตรประชาชน, ");
                validateMsg.Append(@"F = คำเสนอขอเช่าซื้อ ด้านหน้า,");
                validateMsg.Append(@"(T = Consent Model หรือ O = Memo Internal), ");
                validateMsg.Append(@"ครบทั้ง 4 ประเภท");

                return validateMsg.ToString();
            }
            #endregion

            #region ### Validate Customer Required ###
            // ต้องมีอย่างน้อย 1 Customer
            #endregion

            #region ### Validate Customer ID Card ###
            // เลขบัตรประจำตัวประชาชน จะต้องมี 13 หลัก
            #endregion

            #region ### Validate Customer Age ###
            // อายุไม่อยู่ในช่วง
            #endregion

            #region ### Validate Unsend NCB consent ###
            // ยังมีConsent ยังไม่ได้ส่ง
            #endregion

            #region ### Validate Credit Checking on Progress ###
            // กำลังดำเนินการตรวจสอบ Credit Checking อยู่
            #endregion

            #region ### Validate Finish Credit Checking ###
            // ตรวจCreditChk เรียบร้อยแล้ว
            #endregion


            // Validation OK :)
            return string.Empty;
        }

        private MktLoanType UpdateLoanTypeBeforeSubmitToCreditChecking(string appId, string userId,
            CheckNCBAppResource checkNCBApp, MktLoanType loanType)
        {
            if (loanType == null)
            {
                loanType = new MktLoanType()
                {
                    AppId = appId,
                    TypeId = checkNCBApp.LoanType,
                    Status = BusinessConstant.StatusActive,
                    CreateBy = userId,
                    CreateDate = DateTime.Now,

                    PopularBrand = checkNCBApp.PopularBrand,
                    IsCarlessOrEqual10 = checkNCBApp.CarAgeLessThanOrEqual10Years,
                    GroupOccType = checkNCBApp.GroupOccupationType
                };
            }

            loanType.TypeId = checkNCBApp.LoanType;
            loanType.PopularBrand = checkNCBApp.PopularBrand;
            loanType.IsCarlessOrEqual10 = checkNCBApp.CarAgeLessThanOrEqual10Years;
            loanType.GroupOccType = checkNCBApp.GroupOccupationType;

            return loanType;
        }

        private MktCar UpdateCarBeforeSubmitToCreditChecking(string appId, string userId,
            CheckNCBAppResource checkNCBApp, MktCar car)
        {
            if (car == null)
            {
                DateTime updatedDate = DateTime.Now;

                car = new MktCar()
                {
                    AppId = appId,
                    //OldNewCar = checkNCBApp.NewOrOldCar,
                    OldCarVatType = BusinessConstant.NonSelectedDropDownIndex,
                    LicenseNo = string.Empty,
                    ChassisCode = string.Empty,
                    HirePrice = 0,
                    RefPrice = 0,
                    NewCarPrice = 0,
                    AssessmentPrice = 0,
                    Status = BusinessConstant.StatusActive,
                    CreateBy = userId,
                    CreateDate = updatedDate,
                    UpdateBy = userId,
                    UpdateDate = updatedDate
                };
            }

            car.OldNewCar = checkNCBApp.NewOrOldCar;

            return car;
        }

        private MktCustomer UpdateCustomerBeforeSubmitToCreditChecking(string appId, string userId,
            CheckNCBAppResource checkNCBApp, MktCustomer customer)
        {
            customer.NewOrOld = checkNCBApp.NewOrOldCustomer;
            customer.CardType = checkNCBApp.CardType;
            customer.IDCardNo = checkNCBApp.IDCardNo;
            customer.TitleId = checkNCBApp.TitleId;
            customer.FirstNameThai = checkNCBApp.FirstNameThai;
            customer.LastNameThai = checkNCBApp.LastNameThai;
            customer.Sex = checkNCBApp.SexId;
            customer.BirthDate = checkNCBApp.BirthDate;

            if ((customer.Addresses == null) || customer.Addresses.Count == 0)
            {
                customer.Addresses = new MktAddress[]
                {
                        new MktAddress
                        {
                            Id = 1,
                            CustomerId = customer.Id, //app.Customer.Id,
                            AddressType = BusinessConstant.AddressType.Address.ToString(),
                            HouseNo = string.Empty,
                            Floor = string.Empty,
                            RoomNo = string.Empty,
                            Moo = string.Empty,
                            Soi = string.Empty,
                            Road = string.Empty,
                            DistrictId = 0,
                            AmphurId = 0,
                            ProvinceId = 0,
                            Status = BusinessConstant.StatusActive
                        },

                        new MktAddress
                        {
                            Id = 2,
                            CustomerId = customer.Id, //app.Customer.Id,
                            AddressType = BusinessConstant.AddressType.Current.ToString(),
                            HouseNo = string.Empty,
                            Floor = string.Empty,
                            RoomNo = string.Empty,
                            Moo = string.Empty,
                            Soi = string.Empty,
                            Road = string.Empty,
                            DistrictId = 0,
                            AmphurId = 0,
                            ProvinceId = 0,
                            Status = BusinessConstant.StatusActive
                        },

                        new MktAddress
                        {
                            Id = 3,
                            CustomerId = customer.Id, //app.Customer.Id,
                            AddressType = BusinessConstant.AddressType.Debt.ToString(),
                            HouseNo = string.Empty,
                            Floor = string.Empty,
                            RoomNo = string.Empty,
                            Moo = string.Empty,
                            Soi = string.Empty,
                            Road = string.Empty,
                            DistrictId = 0,
                            AmphurId = 0,
                            ProvinceId = 0,
                            Status = BusinessConstant.StatusActive
                        },

                        new MktAddress
                        {
                            Id = 4,
                            CustomerId = customer.Id, //app.Customer.Id,
                            AddressType = BusinessConstant.AddressType.Document.ToString(),
                            AddressCode = checkNCBApp.MailingAddressCode ?? string.Empty,
                            HouseNo = checkNCBApp.MailingHouseNo,
                            Floor = checkNCBApp.MailingFloor ?? string.Empty,
                            RoomNo = checkNCBApp.MailingRoomNo ?? string.Empty,
                            Moo = checkNCBApp.MailingMoo ?? string.Empty,
                            Soi = checkNCBApp.MailingSoi ?? string.Empty,
                            Road = checkNCBApp.MailingRoad ?? string.Empty,
                            DistrictId = checkNCBApp.MailingDistrictId,
                            AmphurId = checkNCBApp.MailingAmphurId,
                            ProvinceId = checkNCBApp.MailingProvinceId,
                            ZipCode = checkNCBApp.MailingZipCode,
                            Status = BusinessConstant.StatusActive
                        },

                        new MktAddress
                        {
                            Id = 5,
                            CustomerId = customer.Id, //app.Customer.Id,
                            AddressType = BusinessConstant.AddressType.Office.ToString(),
                            HouseNo = string.Empty,
                            Floor = string.Empty,
                            RoomNo = string.Empty,
                            Moo = string.Empty,
                            Soi = string.Empty,
                            Road = string.Empty,
                            DistrictId = 0,
                            AmphurId = 0,
                            ProvinceId = 0,
                            Status = BusinessConstant.StatusActive
                        },

                        new MktAddress
                        {
                            Id = 6,
                            CustomerId = customer.Id, //app.Customer.Id,
                            AddressType = BusinessConstant.AddressType.Other.ToString(),
                            HouseNo = string.Empty,
                            Floor = string.Empty,
                            RoomNo = string.Empty,
                            Moo = string.Empty,
                            Soi = string.Empty,
                            Road = string.Empty,
                            DistrictId = 0,
                            AmphurId = 0,
                            ProvinceId = 0,
                            Status = BusinessConstant.StatusActive
                        },

                        new MktAddress
                        {
                            Id = 7,
                            CustomerId = customer.Id, //app.Customer.Id,
                            AddressType = BusinessConstant.AddressType.Person.ToString(),
                            HouseNo = string.Empty,
                            Floor = string.Empty,
                            RoomNo = string.Empty,
                            Moo = string.Empty,
                            Soi = string.Empty,
                            Road = string.Empty,
                            DistrictId = 0,
                            AmphurId = 0,
                            ProvinceId = 0,
                            Status = BusinessConstant.StatusActive
                        }
                };

            }
            else
            {
                MktAddress documentAddress = customer.Addresses
                    .SingleOrDefault(a => a.AddressType == BusinessConstant.AddressType.Document.ToString());

                if (documentAddress != null)
                {
                    documentAddress.AddressCode = checkNCBApp.MailingAddressCode ?? string.Empty;
                    documentAddress.HouseNo = checkNCBApp.MailingHouseNo;
                    documentAddress.Floor = checkNCBApp.MailingFloor ?? string.Empty;
                    documentAddress.RoomNo = checkNCBApp.MailingRoomNo ?? string.Empty;
                    documentAddress.Moo = checkNCBApp.MailingMoo ?? string.Empty;
                    documentAddress.Soi = checkNCBApp.MailingSoi ?? string.Empty;
                    documentAddress.Road = checkNCBApp.MailingRoad ?? string.Empty;
                    documentAddress.DistrictId = checkNCBApp.MailingDistrictId;
                    documentAddress.AmphurId = checkNCBApp.MailingAmphurId;
                    documentAddress.ProvinceId = checkNCBApp.MailingProvinceId;
                    documentAddress.ZipCode = checkNCBApp.MailingZipCode;
                }
            }

            return customer;
        }

        private MktApplication SaveAppBeforeSubmitToCreditChecking(
            MktApplication app, UserProfile userProfile, CheckNCBAppResource checkNCBApp)
        {
            if (app == null)
                return null;

            app.LoanType = UpdateLoanTypeBeforeSubmitToCreditChecking(app.AppId, userProfile.UserId, checkNCBApp,
                app.LoanType);

            app.Car = UpdateCarBeforeSubmitToCreditChecking(app.AppId, userProfile.UserId, checkNCBApp,
                app.Car);

            app.Customer = UpdateCustomerBeforeSubmitToCreditChecking(app.AppId, userProfile.UserId, checkNCBApp,
                app.Customer);

            MktApplication updatedApp = _appService.SaveAppBeforeSubmitToCreditChecking(app, userProfile);

            return updatedApp;
        }

        private bool SendToCreditChecking(
            MktApplication app, UserProfile userProfile, CheckNCBAppResource checkNCBApp)
        {
            if (app == null)
                return false;

            bool hasConsentScoreModel = checkNCBApp.Attachments.Any(a =>
                    a.AttachmentType == BusinessConstant.AttachmentTypeConsentModel);

            _appService.SubmitToCreditChecking(app.AppId, app.Customer.Id, hasConsentScoreModel,
                checkNCBApp.Attachments, userProfile);

            return true; // Success
        }

        [HttpPost("{appId}/creditchecking")]
        public async Task<IActionResult> SubmitToCreditChecking(string appId, 
            [FromBody] CheckNCBAppResource checkNCBApp)
        {
            try
            {
                // 1. Validate Parameters
                if (String.IsNullOrEmpty(appId))
                    return BadRequest(new ErrorMessage { ErrorText = "โปรดระบุเลขที่ใบคำขอ" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // 2. Validate Input
                string validateMsg = ValidateInputForCreditChecking(checkNCBApp);

                // if Validation failed, return Error Message
                if (!String.IsNullOrEmpty(validateMsg))
                {
                    return BadRequest(new ErrorMessage { ErrorText = validateMsg });
                }

                // 3) Save App. Before Submit to Credit Checking
                Log.Information("From Body : appId = {@appId}, CheckNCBAppResource = {@checkNCBApp}", appId, checkNCBApp);

                UserProfile userProfile = await _authService.GetUserProfileFromToken(HttpContext);

                var app = _appService.GetApp(appId);

                var updatedApp = SaveAppBeforeSubmitToCreditChecking(app, userProfile, checkNCBApp);

                // 4. Send to Credit Checking
                bool isOK = SendToCreditChecking(updatedApp, userProfile, checkNCBApp);

                return Ok(updatedApp);
            }
            catch (Exception ex)
            {

                Log.Error("This is {@Exception}", ex);

                return BadRequest(new ErrorMessage { ErrorText = ex.Message });
            }
            
        }

        [HttpPut("{appId}")]
        public IActionResult UpdateApp(string appId)
        {
            //var updatedApp = _repo.FindByKey(appNo);

            //updatedApp.MKT_Application_ActiveContract_AppID = "Test";
            //updatedApp.MKT_Application_DealerID = "333";

            //_repo.Commit();

            //return Ok(updatedApp);

            return NoContent();
        }

        [HttpDelete("{appId}")]
        public IActionResult DeleteApp(string appId)
        {
            //var deletedApp = _repo.FindByKey(appNo);

            //_repo.Remove(deletedApp);

            //_repo.Commit();

            //return Ok();

            return NoContent();
        }

    }
}