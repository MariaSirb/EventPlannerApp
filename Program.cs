using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EventPlannerApp.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("AdminPolicy", policy =>
 policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Clients", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/MenuTypes");
    options.Conventions.AllowAnonymousToPage("/Eventypes/Delete");
    options.Conventions.AllowAnonymousToPage("/Locations/Delete");
    options.Conventions.AllowAnonymousToPage("/Menues/Delete");
    options.Conventions.AllowAnonymousToPage("/Musics/Delete");
    options.Conventions.AllowAnonymousToPage("/Photographs/Delete");
   
}


);
builder.Services.AddDbContext<EventPlannerAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventPlannerAppContext") ?? throw new InvalidOperationException("Connection string 'EventPlannerAppContext' not found.")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//.AddEntityFrameworkStores<LibraryIdentityContext>();

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("EventPlannerAppContext") ?? throw new InvalidOperationException("Connectionstring 'EventPlannerAppContext' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
     .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
