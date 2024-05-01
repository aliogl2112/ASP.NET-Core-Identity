using Identity.Data.Concrete.EFCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>();

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<IdentityContext>();// AddEntityFrameworkStores ile tablolarýn hangi dbye eklendiðini belirtiyoruz

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
