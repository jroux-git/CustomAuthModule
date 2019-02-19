using Microsoft.AspNetCore.Identity;

namespace AspNetCoreAuthModule.Models
{
    public abstract class User: IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}