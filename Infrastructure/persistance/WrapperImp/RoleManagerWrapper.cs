using Application.Wrapper;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.persistance.WrapperImp
{
    public class RoleManagerWrapper : IRoleWrapper
    {
        private readonly RoleManager<IdentityRole<string>> _role;
        public RoleManagerWrapper(RoleManager<IdentityRole<string>> role)
        {
            _role = role;
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            return await _role.RoleExistsAsync(role);
        }
    }
}
