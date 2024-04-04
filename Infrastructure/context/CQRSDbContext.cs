using Domains.Entities;
using Infrastructure.ModelDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.context
{
    public class CQRSDbContext :IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        public CQRSDbContext(DbContextOptions<CQRSDbContext> contextOptions) : base(contextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Customer>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId);
           
        }

        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<Customer> customers { get; set; }

    }
}
