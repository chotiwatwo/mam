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
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            // API Resource to Domain
            CreateMap<CreateAppResource, MktApplication>();
            CreateMap<CreateCustomerResource, MktCustomer>();

            // Domain to API Resource
            CreateMap<MktApplication, CheckNCBAppResource>()
                .ForMember(chk => chk.NewOrOldCustomer,
                    opt => opt.MapFrom(a => a.Customer.NewOrOld))
                .ForMember(chk => chk.CardType,
                    opt => opt.MapFrom(a => a.Customer.CardType))
                .ForMember(chk => chk.IDCardNo,
                    opt => opt.MapFrom(a => a.Customer.IDCardNo))
                .ForMember(chk => chk.TitleId,
                    opt => opt.MapFrom(a => a.Customer.TitleId))
                .ForMember(chk => chk.FirstNameThai,
                    opt => opt.MapFrom(a => a.Customer.FirstNameThai))
                .ForMember(chk => chk.LastNameThai,
                    opt => opt.MapFrom(a => a.Customer.LastNameThai))
                .ForMember(chk => chk.SexId,
                    opt => opt.MapFrom(a => a.Customer.Sex));


        }
    }
}
