 
using Microsoft.AspNetCore.Identity;

namespace Lean.Domain.DBEntities.Administration
{
    public  class User:IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
