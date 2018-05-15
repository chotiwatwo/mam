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
    [Authorize]
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

        [HttpGet("loan-types")]
        public IActionResult GetLoanTypes()
        {
            /*
            B รถซื้อขายกันเอง
            L Low Ticket Size
            P สินเชื่อเช่าซื้อ
            R Top-Up Loan
            S   จำนำ(SLB)
            */

            var loanTypesResource = _service.GetMasterLoanTypes();

            return Ok(loanTypesResource);
        }

        [HttpGet("popular-brand-options")]
        public IActionResult GetPopularBrandOptions()
        {
            var popularBrandOptions = _service.GetYesOrNoOptions();

            return Ok(popularBrandOptions);
        }

        [HttpGet("car-age-options")]
        public IActionResult GetCarAgeOptions()
        {
            var carAgeOptions = _service.GetYesOrNoOptions();

            return Ok(carAgeOptions);
        }

        [HttpGet("group-occupation-options")]
        public IActionResult GetGroupOccupationTypeOptions()
        {
            var groupOccupationTypeOptions = _service.GetGroupOccupationTypeOptions();

            return Ok(groupOccupationTypeOptions);
        }

        [HttpGet("provinces")]
        public IActionResult GetProvinces()
        {
            var provinces = _service.GetProvinces();

            var provincesResource = _mapper.Map<IEnumerable<Province>,IEnumerable<ProvinceResource>>(provinces);

            return Ok(provincesResource);
        }

        [HttpGet("provinces/{provinceId}/amphurs")]
        public IActionResult GetAmphurs(short provinceId)
        {
            var amphurs = _service.GetAmphurs(provinceId);

            var amphursResource = _mapper.Map<IEnumerable<Amphur>, IEnumerable<AmphurResource>>(amphurs);

            return Ok(amphursResource);
        }

        [HttpGet("amphurs/{amphurId}/districts")]
        public IActionResult GetDistricts(long amphurId)
        {
            var districts = _service.GetDistricts(amphurId);

            var districtsResource = _mapper.Map<IEnumerable<District>, IEnumerable<DistrictResource>>(districts);

            return Ok(districtsResource);
        }
    }
}