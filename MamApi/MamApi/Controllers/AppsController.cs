using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MamApi.Services;
using MamApi.Models.Resources;
using AutoMapper;
using MamApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace MamApi.Controllers
{
    
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

        [HttpGet("{appNo}")]
        public IActionResult GetApp(string appNo)
        {
            try
            {
                var app = _appService.GetApp(appNo);

                if (app == null)
                {
                    //return NotFound($"Application No : [{ appNo }] was not found");
                    return NotFound($"ไม่พบเลขที่ใบคำขอ : [{ appNo }] ในระบบ");
                }

                return Ok(app);
            }
            catch
            {

                
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateApp([FromBody] CreateAppResource app)
        {
            string branchId = await _authService.GetBranchIdFromUserProfile(HttpContext);

            var mktApp = _mapper.Map<CreateAppResource, MktApplication>(app);

            var mktCustomer = _mapper.Map<CreateCustomerResource, MktCustomer>(app.Customer);

            mktApp.Customer = mktCustomer;

            var createdApp = _appService.CreateApp(mktApp);

            return Ok(createdApp);
        }

        [HttpPut("{appNo}")]
        public IActionResult UpdateApp(string appNo)
        {
            //var updatedApp = _repo.FindByKey(appNo);

            //updatedApp.MKT_Application_ActiveContract_AppID = "Test";
            //updatedApp.MKT_Application_DealerID = "333";

            //_repo.Commit();

            //return Ok(updatedApp);

            return NoContent();
        }

        [HttpDelete("{appNo}")]
        public IActionResult DeleteApp(string appNo)
        {
            //var deletedApp = _repo.FindByKey(appNo);

            //_repo.Remove(deletedApp);

            //_repo.Commit();

            //return Ok();

            return NoContent();
        }

    }
}