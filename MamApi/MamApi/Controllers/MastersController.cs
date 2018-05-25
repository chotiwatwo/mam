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

        private IEnumerable<MasterInfoResource> GetMasterInfoResource(string infoType)
        {
            var MasterInfos = _service.GetMasterInfosByType(infoType, "A");

            var MasterInfoResources = _mapper.Map<IEnumerable<MasterInfo>, IEnumerable<MasterInfoResource>>(MasterInfos);

            return MasterInfoResources;
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
            var MasterInfoResources = GetMasterInfoResource("Title");

            return Ok(MasterInfoResources);
        }

        // ประเภทเอกสาร (สำหรับ Attach)
        [HttpGet("attachment-types")]
        public IActionResult GetAttachmentTypes()
        {
            var MasterInfoResources = GetMasterInfoResource("AttachmentType");

            return Ok(MasterInfoResources);
        }

        // เชื้อชาติ
        [HttpGet("races")]
        public IActionResult GetRaces()
        {
            var MasterInfoResources = GetMasterInfoResource("Race");

            return Ok(MasterInfoResources);
        }

        // สัญชาติ / ประเทศถิ่นพำนัก / ประเทศแหล่งเงินได้
        [HttpGet("nationalities")]
        public IActionResult GetNationalities()
        {
            var MasterInfoResources = GetMasterInfoResource("Nationality");

            return Ok(MasterInfoResources);
        }

        // ศาสนา
        [HttpGet("religious")]
        public IActionResult GetReligious()
        {
            var MasterInfoResources = GetMasterInfoResource("Religious");

            return Ok(MasterInfoResources);
        }

        // วุฒิการศึกษา
        [HttpGet("education-types")]
        public IActionResult GetEducationTypes()
        {
            var MasterInfoResources = GetMasterInfoResource("EducationType");

            return Ok(MasterInfoResources);
        }

        // สถานภาพการสมรส
        [HttpGet("married-status")]
        public IActionResult GetMarriedStatus()
        {
            var MasterInfoResources = GetMasterInfoResource("MarriedStatus");

            return Ok(MasterInfoResources);
        }

        // ภาระรับผิดชอบ
        [HttpGet("responsibilities")]
        public IActionResult GetResponsibilities()
        {
            var MasterInfoResources = GetMasterInfoResource("Responsibility");

            return Ok(MasterInfoResources);
        }

        // ประเภทโทรศัพท์
        [HttpGet("telephone-types")]
        public IActionResult GetTelephoneTypes()
        {
            var MasterInfoResources = GetMasterInfoResource("TelephoneType");

            return Ok(MasterInfoResources);
        }

        // ประเภทอีเมลล์
        [HttpGet("email-types")]
        public IActionResult GetEmailTypes()
        {
            var MasterInfoResources = GetMasterInfoResource("EmailType");

            return Ok(MasterInfoResources);
        }

        // สถานะในที่อยู่อาศัย 
        [HttpGet("living-status")]
        public IActionResult GetLivingStatus()
        {
            var MasterInfoResources = GetMasterInfoResource("LivingStatus");

            return Ok(MasterInfoResources);
        }

        // กรรมสิทธิ์ในสิ่งปลูกสร้าง
        [HttpGet("house-ownerships")]
        public IActionResult GetHouseOwnerships()
        {
            var MasterInfoResources = GetMasterInfoResource("HouseOwnership");

            return Ok(MasterInfoResources);
        }

        // สถานภาพการจ้างงาน
        [HttpGet("hire-status")]
        public IActionResult GetHireStatusCodes()
        {
            var MasterInfoResources = GetMasterInfoResource("HireStatusCode");

            return Ok(MasterInfoResources);
        }

        // ขนาดกิจการ
        [HttpGet("company-sizes")]
        public IActionResult GetCompanySizeCodes()
        {
            var MasterInfoResources = GetMasterInfoResource("CompanySizeCode");

            return Ok(MasterInfoResources);
        }

        [HttpGet("business-sectors")]
        public IActionResult GetBusinessSectors()
        {
            var MasterInfoResources = GetMasterInfoResource("BusinessSector");

            return Ok(MasterInfoResources);
        }

        //[HttpGet("")]
        //public IActionResult Get()
        //{
        //    var MasterInfoResources = GetMasterInfoResource("");

        //    return Ok(MasterInfoResources);
        //}

        //[HttpGet("")]
        //public IActionResult Get()
        //{
        //    var MasterInfoResources = GetMasterInfoResource("");

        //    return Ok(MasterInfoResources);
        //}

        //[HttpGet("")]
        //public IActionResult Get()
        //{
        //    var MasterInfoResources = GetMasterInfoResource("");

        //    return Ok(MasterInfoResources);
        //}

        // 

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