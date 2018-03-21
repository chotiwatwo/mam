using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MamApi.Services;
using MamApi.Models.Resources;
using AutoMapper;
using MamApi.Models;

namespace MamApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Apps")]
    public class AppsController : Controller
    {
        private readonly IAppService AppService;
        private readonly IMapper mapper;

        public AppsController(IAppService appService, IMapper mapper)
        {
            this.AppService = appService;
            this.mapper = mapper;
        }

        [HttpGet("test")]
        public IActionResult test() {
            return Ok("Yes");
        }

        [HttpGet]
        public IActionResult GetApps() {
            var apps = AppService.GetApps();

            return Ok(apps);
        }

        [HttpGet("{appNo}")]
        public IActionResult GetApp(string appNo)
        {
            try
            {
                var app = AppService.GetApp(appNo);

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

        [HttpPost]
        public IActionResult CreateApp([FromBody] CreateAppResource app) {
            //var createdApp = _repo.Add(app);

            //_repo.Commit();

            var mktApp = mapper.Map<CreateAppResource, MktApplication>(app);

            var mktCustomer = mapper.Map<CreateCustomerResource, MktCustomer>(app.Customer);

            mktApp.Customer = mktCustomer;

            var createdApp = AppService.CreateApp(mktApp);

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