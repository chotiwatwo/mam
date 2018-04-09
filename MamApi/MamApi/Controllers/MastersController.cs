using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MamApi.Models;
using MamApi.Models.Resources;
using MamApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MamApi.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/masters")]
    public class MastersController : Controller
    {
        private readonly IMasterService _service;
        private readonly IMapper _mapper;

        public MastersController(IMasterService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("card-types")]
        public IActionResult GetCardTypes() {
            var CardTypes = _service.GetMasterInfosByType("IDType", "A");

            if (CardTypes == null || !CardTypes.Any()) {
                return NoContent();
            }

            var CardTypesResource = _mapper.Map<IEnumerable<MasterInfo>, IEnumerable<MasterInfoResource>>(CardTypes);

            return Ok(CardTypesResource);
        }

        [HttpGet("titles")]
        public IActionResult GetTitles()
        {
            var Titles = _service.GetMasterInfosByType("Title", "A");

            var TitlesResource = _mapper.Map<IEnumerable<MasterInfo>, IEnumerable<MasterInfoResource>>(Titles);

            return Ok(TitlesResource);
        }
    }
}