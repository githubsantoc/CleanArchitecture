using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.repository
{
    public interface IUserRepo
    {
        Task<List<User>> ToListAs();
    }
}
