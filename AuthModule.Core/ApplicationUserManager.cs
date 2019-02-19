using AspNetCoreAuthModule.Intefaces;
using AspNetCoreAuthModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AspNetCoreAuthModule
{
    public class ApplicationUserManager: UserManager<ApplicationUser>, IApplicationUserManager
    {
        //private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, 
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
        : base(
            store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            //_userManager = new UserManager<ApplicationUser>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger);
        }

        public async  override Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await FindByIdAsync(id);
        }

        public async override Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindByUsernameAsync(string username)
        {
            return await FindByNameAsync(username);
        }

        public async override Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password)
        {
            return await CreateAsync(applicationUser, password);
        }
    }
}
