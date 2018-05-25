using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MamApi.Services;
using MamApi.Models.Resources;
using AutoMapper;
using MamApi.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using Serilog;

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

        [HttpGet("test")]
        public IActionResult test() {
            return Ok("Yes");
        }

        [HttpGet]
        public IActionResult GetApps() {
            var apps = _appService.GetApps();

            return Ok(apps);
        }

        [HttpGet("{appId}", Name = "GetApp")]
        public IActionResult GetApp(string appId, bool toCheckNCB = false)
        {
            try
            {
                //Log.Information("This is {@appNo}", appNo);
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

                var checkNCBAppResource = _mapper.Map<MktApplication, CheckNCBAppResource>(app);

                //return Ok(new { checkNcbApp = checkNCBAppResource, fullApp = app });

                return Ok(new { checkNcbApp = checkNCBAppResource });
            }
            catch (Exception ex)
            {
                Log.Error("มี error {@Except}", ex);
            }

            return BadRequest();
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

        [HttpPost("{appId}/creditchecking")]
        public async Task<IActionResult> SubmitToCreditChecking(string appId, 
            [FromBody] CheckNCBAppResource checkNCBApp)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // 1) Validate 

                // 2) Save


                return Ok(checkNCBApp);
            }
            catch (Exception ex)
            {

                Log.Error("This is {@Exception}", ex);
            }

            return BadRequest();
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