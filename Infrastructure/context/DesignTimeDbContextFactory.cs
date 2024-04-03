﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CQRSDbContext>
    {
        public CQRSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CQRSDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Database=CleanArchitecture;User Id=postgres;Password=password");

            return new CQRSDbContext(optionsBuilder.Options);
        }
    }
}