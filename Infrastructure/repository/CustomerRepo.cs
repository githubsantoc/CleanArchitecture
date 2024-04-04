using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Entities;
using Domains.repository;
using Infrastructure.context;
using Infrastructure.ModelDto;
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

        public async Task AddAsy(Customer customer)
        {
            await context.customers.AddAsync(customer);
            await context.SaveChangesAsync();
        }

        public async Task FirstOrDefaultAsy(Customer customer)
        {
            await context.customers.FirstOrDefaultAsync(x => x.Id == customer.Id);
            await context.SaveChangesAsync();
        }

        public async Task<List<Customer>> ToListAsy()
        {
            var list = await context.customers.ToListAsync();
            return list;
        }
    }
}
