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
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<User, UserProfileResource>();
        }
    }
}
