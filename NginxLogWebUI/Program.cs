var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

#region services config

services.AddControllersWithViews().AddRazorRuntimeCompilation();

#endregion

var app = builder.Build();

#region app config

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion

