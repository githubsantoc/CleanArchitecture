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

namespace Infrastructure.persistance
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

        public async Task<List<Customer>> ToListAsy()
        {
            var list = await context.customers.ToListAsync();
            return list;
        }

        public async Task<Customer?> FirstOrDefaultAsy(int id)
        {
            return await context.customers.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task UpdateAsy(Customer customer)
        {
            context.customers.Update(customer);
            await context.SaveChangesAsync();
        }
    }
}
