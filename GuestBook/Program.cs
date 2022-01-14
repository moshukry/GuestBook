using Microsoft.EntityFrameworkCore;
using GuestBook.Models ;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<GuestBookContext>(option => option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("guestbookcon")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/{statusCode}");
    app.UseStatusCodePagesWithRedirects("/Error/{0}");
    app.UseHsts();
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Operations}/{action=LogIn}/{id?}");

app.Run();
