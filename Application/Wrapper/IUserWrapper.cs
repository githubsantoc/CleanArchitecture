using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper
{
    public interface IUserWrapper
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);

        Task<IdentityResult> AddToRoleAsync(User user, string role);

        Task<User> FindByEmailAs( string email);
        Task<bool> CheckPWAsync(User user, string password);
        Task<IList<string>> GetRolesAs(User user);
        Task<IdentityResult> UpdateAs(User user);
        
       
    }
}

