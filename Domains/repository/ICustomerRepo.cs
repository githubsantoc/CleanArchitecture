using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.repository
{
    public interface ICustomerRepo
    {
        Task AddAsy(Customer customer);
        Task<Customer?> FirstOrDefaultAsy(int id); 
        Task<List<Customer>> ToListAsy();
        Task UpdateAsy(Customer customer);
    }
}
