using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Models;
using MamApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MamApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Apps")]
    public class AppsController : Controller
    {
        private readonly IRepository<MktApplication> _repo;

        public AppsController(IRepository<MktApplication> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetApps() {
            var apps = _repo.FetchAll();

            return Ok(apps);
        }

        [HttpGet("{appNo}")]
        public IActionResult GetApp(string appNo)
        {
            var app = _repo.FindByKey(appNo);

            //var app = _repo.Query(a => a.MKT_Application_CurrentCarID == 138690);

            return Ok(app);
        }

        [HttpPost]
        public IActionResult CreateApp([FromBody] MktApplication app) {
            var createdApp = _repo.Add(app);

            _repo.Save();

            return Ok(createdApp);
        }

        [HttpPut("{appNo}")]
        public IActionResult UpdateApp(string appNo)
        {
            var updatedApp = _repo.FindByKey(appNo);

            updatedApp.MKT_Application_ActiveContract_AppID = "Test";
            updatedApp.MKT_Application_DealerID = "333";

            _repo.Save();

            return Ok(updatedApp);
        }

        [HttpDelete("{appNo}")]
        public IActionResult DeleteApp(string appNo)
        {
            var deletedApp = _repo.FindByKey(appNo);

            _repo.Remove(deletedApp);

            _repo.Save();

            return Ok();
        }

    }
}