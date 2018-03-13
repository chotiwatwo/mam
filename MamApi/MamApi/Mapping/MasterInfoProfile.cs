using AutoMapper;
using MamApi.Models;
using MamApi.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Mapping
{
    public class MasterInfoProfile : Profile
    {
        public MasterInfoProfile()
        {
            // Domain to API Resource 
            CreateMap<MasterInfo, MasterInfoResource>();
        }
    }
}
