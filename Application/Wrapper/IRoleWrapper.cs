using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper
{
    public interface IRoleWrapper
    {
        Task<bool> RoleExistsAsync(string role);
    }
}
