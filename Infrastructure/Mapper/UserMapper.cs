using AutoMapper;
using Domains.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, ApplicationUser>();

            CreateMap<ApplicationUser, User>();
        }
    }
}

