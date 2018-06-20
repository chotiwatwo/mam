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
            #region <<<API Resource to Domain>>> To map input data from Client for Insert, Update, Delete to DB
            
            CreateMap<CreateAppResource, MktApplication>();
            CreateMap<CreateCustomerResource, MktCustomer>();

            #endregion API Resource to Domain data

            #region <<<Domain to API Resource>>> To map query result from DB and return to Client
            // GetApp to CheckNCB  => appService.GetAppToCheckNCB() 
            CreateMap<MktApplication, CheckNCBAppResource>()
                .ForMember(chk => chk.NewOrOldCustomer,
                    opt => opt.MapFrom(a => a.Customer.NewOrOld))
                .ForMember(chk => chk.CardType,
                    opt => opt.MapFrom(a => a.Customer.CardTypeId))
                .ForMember(chk => chk.IDCardNo,
                    opt => opt.MapFrom(a => a.Customer.IDCardNo))
                .ForMember(chk => chk.TitleId,
                    opt => opt.MapFrom(a => a.Customer.TitleId))
                .ForMember(chk => chk.FirstNameThai,
                    opt => opt.MapFrom(a => a.Customer.FirstNameThai))
                .ForMember(chk => chk.LastNameThai,
                    opt => opt.MapFrom(a => a.Customer.LastNameThai))
                .ForMember(chk => chk.SexId,
                    opt => opt.MapFrom(a => a.Customer.SexId));

            // Get Full App => appService.GetApp()
            CreateMap<MktApplication, ApplicationResource>();
                //.ForMember(ar => ar.Branch,
                //    opt => opt.MapFrom(ma => ma.Branch));

            CreateMap<MktCustomer, CustomerResource>();

            CreateMap<MktAddress, AddressResource>();

            #endregion Domain to API Resource

            // API Resource to Domain
            //CreateMap<CheckNCBAppResource, MktApplication>()
            //    .ForMember(a => a.Customer.NewOrOld,
            //        opt => opt.MapFrom(chk => chk.NewOrOldCustomer))
            //    .ForMember(a => a.Customer.CardType,
            //        opt => opt.MapFrom(chk => chk.CardType))
            //    .ForMember(a => a.Customer.IDCardNo,
            //        opt => opt.MapFrom(chk => chk.IDCardNo))
            //    .ForMember(a => a.Customer.TitleId,
            //        opt => opt.MapFrom(chk => chk.TitleId))
            //    .ForMember(a => a.Customer.FirstNameThai,
            //        opt => opt.MapFrom(chk => chk.FirstNameThai))
            //    .ForMember(a => a.Customer.LastNameThai,
            //        opt => opt.MapFrom(chk => chk.LastNameThai))
            //    .ForMember(a => a.Customer.Sex,
            //        opt => opt.MapFrom(chk => chk.SexId));

            //.ForMember(a => a.Customer.Addresses,
            //    opt => opt.ResolveUsing())
        }
    }
}
