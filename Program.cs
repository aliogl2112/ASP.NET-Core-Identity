using Identity.Data.Concrete.EFCore;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>();

builder.Services.AddIdentity<User,Role>().AddEntityFrameworkStores<IdentityContext>();// AddEntityFrameworkStores ile tablolarýn hangi dbye eklendiðini belirtiyoruz
//User yerinde ilk önce IdentityUser yazýyorduk. Artýk IdentityUser'den türettiðimiz daha kapsamlý User classý ile çalýþtýðýmýz için deðerini deðiþtirdik.
builder.Services.Configure<IdentityOptions>(options => //Identity ayarlamalarý burdan yapýlýyor.
{
    //parola ayarlarý
    options.Password.RequiredLength = 8; //min length
    options.Password.RequireNonAlphanumeric = false; //özel karakter
    options.Password.RequireLowercase = false; // küçük harf
	options.Password.RequireUppercase = false; // büyük harf
	options.Password.RequireDigit = true; // rakam

    //kullanýcý ayarlarý
    options.User.RequireUniqueEmail=true;//unique email
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "; //username içerisinde kullanýlabilecek karakterler
    
    //lockout ayarlarý
    options.Lockout.MaxFailedAccessAttempts = 5; // max giriþ deneme hakký
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // hak dolduktan sonra yeni deneme için beklenecek süre
    options.Lockout.AllowedForNewUsers = false;

    //signin ayarlarý
    options.SignIn.RequireConfirmedPhoneNumber = false; //giriþ için telefonunun onaylý olmasý
	options.SignIn.RequireConfirmedEmail = false; //giriþ için mailinin onaylý olmasý

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

IdentitySeedData.IdentityTestUser(app); //Seeddatayý çaðýrdýk ve beklediði app nesnesini gönderdik

app.Run();
