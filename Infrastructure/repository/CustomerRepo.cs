using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.repository;
using Infrastructure.context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CQRSDbContext context;

        public CustomerRepo(CQRSDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            /*await context.customers.AddAsync(customer);*/
            await context.SaveChangesAsync();
        }
    }
}
