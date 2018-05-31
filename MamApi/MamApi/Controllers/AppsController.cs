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
                    return Ok(app);
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

            return customer;
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

                app.LoanType = UpdateLoanTypeBeforeSubmitToCreditChecking(appId, userProfile.UserId, checkNCBApp, 
                    app.LoanType);

                app.Car = UpdateCarBeforeSubmitToCreditChecking(appId, userProfile.UserId, checkNCBApp, 
                    app.Car);

                app.Customer = UpdateCustomerBeforeSubmitToCreditChecking(appId, userProfile.UserId, checkNCBApp,
                    app.Customer);

                //if (app.LoanType == null)
                //{
                //    app.LoanType = new MktLoanType()
                //    {
                //        AppId = appId,
                //        TypeId = checkNCBApp.LoanType,
                //        Status = BusinessConstant.StatusActive,
                //        CreateBy = userProfile.UserId,
                //        CreateDate = DateTime.Now,

                //        PopularBrand = checkNCBApp.PopularBrand,
                //        IsCarlessOrEqual10 = checkNCBApp.CarAgeLessThanOrEqual10Years,
                //        GroupOccType = checkNCBApp.GroupOccupationType
                //    };
                //}

                //app.LoanType.TypeId = checkNCBApp.LoanType;
                //app.LoanType.PopularBrand = checkNCBApp.PopularBrand;
                //app.LoanType.IsCarlessOrEqual10 = checkNCBApp.CarAgeLessThanOrEqual10Years;
                //app.LoanType.GroupOccType = checkNCBApp.GroupOccupationType;

                //if (app.Car == null)
                //{
                //    DateTime updatedDate = DateTime.Now;

                //    app.Car = new MktCar()
                //    {
                //        AppId = appId,
                //        //OldNewCar = checkNCBApp.NewOrOldCar,
                //        OldCarVatType = BusinessConstant.NonSelectedDropDownIndex,
                //        LicenseNo = string.Empty,
                //        ChassisCode = string.Empty,
                //        HirePrice = 0,
                //        RefPrice = 0,
                //        NewCarPrice = 0,
                //        AssessmentPrice = 0,
                //        Status = BusinessConstant.StatusActive,
                //        CreateBy = userProfile.UserId,
                //        CreateDate = updatedDate,
                //        UpdateBy = userProfile.UserId,
                //        UpdateDate = updatedDate
                //    };
                //}

                //app.Car.OldNewCar = checkNCBApp.NewOrOldCar;

                //app.Customer.NewOrOld = checkNCBApp.NewOrOldCustomer;
                //app.Customer.CardType = checkNCBApp.CardType;
                //app.Customer.IDCardNo = checkNCBApp.IDCardNo;
                //app.Customer.TitleId = checkNCBApp.TitleId;
                //app.Customer.FirstNameThai = checkNCBApp.FirstNameThai;
                //app.Customer.LastNameThai = checkNCBApp.LastNameThai;
                //app.Customer.Sex = checkNCBApp.SexId;
                //app.Customer.BirthDate = checkNCBApp.BirthDate;

                //if ((app.Customer.Addresses == null) || app.Customer.Addresses.Count == 0)
                //{
                //    app.Customer.Addresses = new MktAddress[]
                //    {
                //        new MktAddress
                //        {
                //            Id = 1,
                //            CustomerId = app.Customer.Id,
                //            AddressType = BusinessConstant.AddressType.Address.ToString(),
                //            HouseNo = string.Empty,
                //            Floor = string.Empty,
                //            RoomNo = string.Empty,
                //            Moo = string.Empty,
                //            Soi = string.Empty,
                //            Road = string.Empty,
                //            DistrictId = 0,
                //            AmphurId = 0,
                //            ProvinceId = 0,
                //            Status = BusinessConstant.StatusActive
                //        },

                //        new MktAddress
                //        {
                //            Id = 2,
                //            CustomerId = app.Customer.Id,
                //            AddressType = BusinessConstant.AddressType.Current.ToString(),
                //            HouseNo = string.Empty,
                //            Floor = string.Empty,
                //            RoomNo = string.Empty,
                //            Moo = string.Empty,
                //            Soi = string.Empty,
                //            Road = string.Empty,
                //            DistrictId = 0,
                //            AmphurId = 0,
                //            ProvinceId = 0,
                //            Status = BusinessConstant.StatusActive
                //        },

                //        new MktAddress
                //        {
                //            Id = 3,
                //            CustomerId = app.Customer.Id,
                //            AddressType = BusinessConstant.AddressType.Debt.ToString(),
                //            HouseNo = string.Empty,
                //            Floor = string.Empty,
                //            RoomNo = string.Empty,
                //            Moo = string.Empty,
                //            Soi = string.Empty,
                //            Road = string.Empty,
                //            DistrictId = 0,
                //            AmphurId = 0,
                //            ProvinceId = 0,
                //            Status = BusinessConstant.StatusActive
                //        },

                //        new MktAddress
                //        {
                //            Id = 4,
                //            CustomerId = app.Customer.Id,
                //            AddressType = BusinessConstant.AddressType.Document.ToString(),
                //            AddressCode = checkNCBApp.MailingAddressCode ?? string.Empty,
                //            HouseNo = checkNCBApp.MailingHouseNo,
                //            Floor = checkNCBApp.MailingFloor ?? string.Empty,
                //            RoomNo = checkNCBApp.MailingRoomNo ?? string.Empty,
                //            Moo = checkNCBApp.MailingMoo ?? string.Empty,
                //            Soi = checkNCBApp.MailingSoi ?? string.Empty,
                //            Road = checkNCBApp.MailingRoad ?? string.Empty,
                //            DistrictId = checkNCBApp.MailingDistrictId,
                //            AmphurId = checkNCBApp.MailingAmphurId,
                //            ProvinceId = checkNCBApp.MailingProvinceId,
                //            ZipCode = checkNCBApp.MailingZipCode,
                //            Status = BusinessConstant.StatusActive
                //        },

                //        new MktAddress
                //        {
                //            Id = 5,
                //            CustomerId = app.Customer.Id,
                //            AddressType = BusinessConstant.AddressType.Office.ToString(),
                //            HouseNo = string.Empty,
                //            Floor = string.Empty,
                //            RoomNo = string.Empty,
                //            Moo = string.Empty,
                //            Soi = string.Empty,
                //            Road = string.Empty,
                //            DistrictId = 0,
                //            AmphurId = 0,
                //            ProvinceId = 0,
                //            Status = BusinessConstant.StatusActive
                //        },

                //        new MktAddress
                //        {
                //            Id = 6,
                //            CustomerId = app.Customer.Id,
                //            AddressType = BusinessConstant.AddressType.Other.ToString(),
                //            HouseNo = string.Empty,
                //            Floor = string.Empty,
                //            RoomNo = string.Empty,
                //            Moo = string.Empty,
                //            Soi = string.Empty,
                //            Road = string.Empty,
                //            DistrictId = 0,
                //            AmphurId = 0,
                //            ProvinceId = 0,
                //            Status = BusinessConstant.StatusActive
                //        },

                //        new MktAddress
                //        {
                //            Id = 7,
                //            CustomerId = app.Customer.Id,
                //            AddressType = BusinessConstant.AddressType.Person.ToString(),
                //            HouseNo = string.Empty,
                //            Floor = string.Empty,
                //            RoomNo = string.Empty,
                //            Moo = string.Empty,
                //            Soi = string.Empty,
                //            Road = string.Empty,
                //            DistrictId = 0,
                //            AmphurId = 0,
                //            ProvinceId = 0,
                //            Status = BusinessConstant.StatusActive
                //        }
                //    };
                //}

                var updatedApp = _appService.SaveAppBeforeSubmitToCreditChecking(app, userProfile);

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