using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PR.OpenGym.API.Configurations;
using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.API.Data;
using PR.OpenGym.API.Repository;
using PR.OpenGym.API.Service;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionMysql") ?? throw new InvalidOperationException("Connection string 'DefaultConnectionMysql' not found.");
var googleCredentials = builder.Configuration.GetSection("googleDriveConfig:googleServiceKeys");
var config = builder.Configuration.GetSection("GoogleDriveConfig:GoogleServiceKeys");
var config1 = config.Get<Dictionary<string, string>>();

var sharedDirectoryId = builder.Configuration.GetSection("GoogleDriveConfig:SharedDirectory");
var sharedDirectoryId1 = sharedDirectoryId.Get<string>();

if (config1 == null || sharedDirectoryId1 == null)
    throw new ArgumentNullException("GoogleServiceKeys and SharedDirectory required");
string jsonKeys = JsonConvert.SerializeObject(config1);
// Add services to the container.
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PROpenGymWebContext>(
    options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

//Repositories Injection
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IAssociateRepository,AssociateRepository>();
builder.Services.AddScoped<IAssociateDetailsRepository,AssociateDetailsRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IBranchRepository,BranchRepository>();
builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();



//Services Injection
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IAssociateService, AssociateService>();
builder.Services.AddScoped<IAssociateDetailsService, AssociateDetailsService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//google service

builder.Services.AddScoped<IImageStorageService>(obj => new GoogleDriveImageStorageService(jsonKeys, sharedDirectoryId1));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
