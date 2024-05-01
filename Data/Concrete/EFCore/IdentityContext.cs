using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data.Concrete.EFCore
{
    public class IdentityContext:IdentityDbContext<IdentityUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //connection stringi dışarıdan almıyoruz direkt context içerisinde tanımlıyoruz
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=IdentityDb;Trusted_Connection=true;TrustServerCertificate=true");
        }
    }
}
