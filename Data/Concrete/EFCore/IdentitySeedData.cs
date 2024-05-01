using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data.Concrete.EFCore
{
    public class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Admin_123";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>(); //Identity context servisine eriştik
            if (context.Database.GetPendingMigrations().Any())//bekleyen migration var mı?
            {
                context.Database.Migrate(); //dotnet ef database update komutuna karşılık geliyor

            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<User>>(); //kullanıcı işlemlerinin yürütüleceği UserManager objesini app üzerinden aldık

            var user = await userManager.FindByNameAsync(adminUser); //kullanıcı adına göre veritabanında arama yapıyoruz

            if(user is null)
            {
                user = new User //Identity framework üzerinden gelen hazır sınıf ile kullanıcı oluşturuyoruz
                {
                    FullName = "Ali Oğul",
                    UserName=adminUser,
                    Email ="admin@aliogul.com",
                    PhoneNumber="12345678900"
                };
                await userManager.CreateAsync(user,adminPassword); //kullanıcı oluştururken user objesi ve parola bilgisi tanımlanır. Kullanıcı login işlemi yapacaksa 2. parametre olarak passwordun verilmesi gerekiyor.
            }
        }
    }
}
