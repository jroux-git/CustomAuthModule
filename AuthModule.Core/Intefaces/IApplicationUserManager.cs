using AspNetCoreAuthModule.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAuthModule.Intefaces
{
    public interface IApplicationUserManager
    {
        Task<ApplicationUser> FindByIdAsync(string id);
        Task<ApplicationUser> FindByUsernameAsync(string username);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password);

    }
}
