using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Models;
using MamApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MamApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SalesmansController : Controller
    {
        private ISalesmanRepository _repo;

        public SalesmansController(ISalesmanRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("", Name = "SalesmanAll")]
        public IActionResult Get() {
            return Ok(_repo.All());
        }

        [HttpGet("{salesmanId}", Name = "SalesmanOne")]
        public IActionResult Get(string salesmanId) {
            var salesman = _repo.GetSalesman(salesmanId);

            if (salesman == null) return NotFound($"ไม่พบ Salesman Id : {salesmanId}");

            return Ok(salesman);
        }
    }
}