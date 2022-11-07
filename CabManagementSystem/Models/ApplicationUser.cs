using Microsoft.AspNetCore.Identity;

namespace CabManagementSystem.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
        public string City { get; set; }
        public bool isDriver { get; set; }
    }
}
