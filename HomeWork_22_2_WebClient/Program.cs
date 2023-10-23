global using HomeWork_22_2_WebClient.Models;
global using HomeWork_22_2_WebClient.Data;
global using HomeWork_22_2_WebClient.Interfaces;
global using HomeWork_22_2_WebClient.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IPhoneBook, PhoneBookApi>();
builder.Services.AddSingleton<IAppUser, AppUserApi>();
builder.Services.AddSingleton<IRoleUser, RoleUserApi>();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseStaticFiles();

//app.UseRouting();

app.UseMvc(
    routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=PhoneBook}/{action=Index}/{id?}");
        routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
    });
    app.Run();
