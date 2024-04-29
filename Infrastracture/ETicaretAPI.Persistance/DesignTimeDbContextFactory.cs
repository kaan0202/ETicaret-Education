﻿using ETicaretAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretAPIDbContext>
    {
        public ETicaretAPIDbContext CreateDbContext(string[] args)
        {
           

            DbContextOptionsBuilder<ETicaretAPIDbContext> optionsBuilder = new();
            optionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(optionsBuilder.Options);
        }
    }
}
