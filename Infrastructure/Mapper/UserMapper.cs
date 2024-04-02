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
            CreateMap<User, ApplicationUser>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
           .ForMember(dest => dest.LoginDateTime, opt => opt.MapFrom(src => src.LoginDateTime));

            CreateMap<ApplicationUser, User>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
           .ForMember(dest => dest.LoginDateTime, opt => opt.MapFrom(src => src.LoginDateTime));
        }
    }
}

