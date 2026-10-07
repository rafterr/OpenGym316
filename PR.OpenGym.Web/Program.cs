using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using PR.OpenGym.Web.Areas.Identity.Data;
using PR.OpenGym.Web.Configurations;
using PR.OpenGym.Web.Data;
using PR.OpenGym.Web.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionMysql") ?? throw new InvalidOperationException("Connection string 'DefaultConnectionMysql' not found.");

var endpoint = builder.Configuration.GetSection("Endpoints").GetValue<string>("Online");
//var endpoint = builder.Configuration.GetSection("Endpoints").GetValue<string>("Dev");

builder.Services.AddHttpClient<IApiService, APIService>((provider, client) => client.BaseAddress = new Uri(endpoint));


var faceEndpoint = builder.Configuration.GetSection("Endpoints:FaceTunnel");
var urlTunnel = faceEndpoint.GetValue<string>("Url");
var usrTunnel = faceEndpoint.GetValue<string>("Usr");
var pwdTunnel = faceEndpoint.GetValue<string>("Pwd");

//builder.Services.AddHttpClient<IFaceTerminalService,FakeIFaceTerminalService>((provider, client) =>
builder.Services.AddHttpClient<IFaceTerminalService, FaceTerminalService>((provider, client) =>
{
    client.BaseAddress = new Uri(urlTunnel);

}).ConfigurePrimaryHttpMessageHandler(_ =>
{
    var handler = new HttpClientHandler();
    var credentialCache = new CredentialCache();
    credentialCache.Add(new Uri(urlTunnel), "Digest", new NetworkCredential(usrTunnel, pwdTunnel, "localhost"));
    handler.Credentials = credentialCache;
    return handler;
});

builder.Services.AddDbContext<PROpenGymWebContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDefaultIdentity<PROpenGymWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PROpenGymWebContext>();

builder.Services.AddAutoMapper(typeof(MyMapperConfiguration));

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
