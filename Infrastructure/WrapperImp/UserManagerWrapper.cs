using Application.Wrapper;
using AutoMapper;
using Domains.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WrapperImp
{
    public class UserManagerWrapper : IUserWrapper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserManagerWrapper(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            return await _userManager.AddToRoleAsync(applicationUser, role);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            return await _userManager.CreateAsync(applicationUser, password); ;
        }
    }
}
