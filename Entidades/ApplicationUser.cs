using Microsoft.AspNetCore.Identity;

namespace Agro.Entidades
{
    public class ApplicationUser : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
    }
}
