using Identity.Data.Concrete.EFCore;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>();

builder.Services.AddIdentity<User,Role>().AddEntityFrameworkStores<IdentityContext>();// AddEntityFrameworkStores ile tablolar�n hangi dbye eklendi�ini belirtiyoruz
//User yerinde ilk �nce IdentityUser yaz�yorduk. Art�k IdentityUser'den t�retti�imiz daha kapsaml� User class� ile �al��t���m�z i�in de�erini de�i�tirdik.
builder.Services.Configure<IdentityOptions>(options => //Identity ayarlamalar� burdan yap�l�yor.
{
    //parola ayarlar�
    options.Password.RequiredLength = 8; //min length
    options.Password.RequireNonAlphanumeric = false; //�zel karakter
    options.Password.RequireLowercase = false; // k���k harf
	options.Password.RequireUppercase = false; // b�y�k harf
	options.Password.RequireDigit = true; // rakam

    //kullan�c� ayarlar�
    options.User.RequireUniqueEmail=true;//unique email
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "; //username i�erisinde kullan�labilecek karakterler
    
    //lockout ayarlar�
    options.Lockout.MaxFailedAccessAttempts = 5; // max giri� deneme hakk�
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // hak dolduktan sonra yeni deneme i�in beklenecek s�re
    options.Lockout.AllowedForNewUsers = false;

    //signin ayarlar�
    options.SignIn.RequireConfirmedPhoneNumber = false; //giri� i�in telefonunun onayl� olmas�
	options.SignIn.RequireConfirmedEmail = false; //giri� i�in mailinin onayl� olmas�

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

IdentitySeedData.IdentityTestUser(app); //Seeddatay� �a��rd�k ve bekledi�i app nesnesini g�nderdik

app.Run();
