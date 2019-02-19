using AspNetCoreAuthModule.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreAuthModule.Database
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions _dbContextOptions;


        public ApplicationIdentityDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
    }
}
