using Application.Wrapper;
using AutoMapper;
using Domains.Entities;
using Infrastructure.ModelDto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistance.WrapperImp
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
            var applicationUser = await _userManager.FindByEmailAsync(user.Email);
            return await _userManager.AddToRoleAsync(applicationUser, role);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            return await _userManager.CreateAsync(applicationUser, password); ;
        }

        public async Task<User> FindByEmailAs(string email)
        {
            var appUser = await _userManager.FindByEmailAsync(email.ToUpperInvariant());
            return _mapper.Map<User>(appUser);
        }

        public async Task<bool> CheckPWAsync(User user, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(user.Email);
            return await _userManager.CheckPasswordAsync(applicationUser, password);
        }

        public async Task<IList<string>> GetRolesAsy(User user)
        {
            var applicationUser = await _userManager.FindByEmailAsync(user.Email);
            return await _userManager.GetRolesAsync(applicationUser);
        }

        public async Task<IdentityResult> UpdateAs(User user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            appUser.LoginDateTime = user.LoginDateTime;
            return await _userManager.UpdateAsync(appUser);
        }

        public async Task<User> FindByNameAsy(string username)
        {
            var appUser = await _userManager.FindByNameAsync(username);
            return _mapper.Map<User>(appUser);

        }

        public async Task<List<User>> GetUsersInRoleAsy(string role)
        {
            var appUser = await _userManager.GetUsersInRoleAsync(role);
            return _mapper.Map<List<User>>(appUser);
        }

        public async Task<List<User>> ToListAs()
        {
            var user = await _userManager.Users.ToListAsync();
            var appUser = _mapper.Map<List<User>>(user);
            return appUser;
        }


    }
}
