using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Permissions.Data;
using Permissions.Data.UnitOfWork;
using Permissions.Services.GroupServices;
using Permissions.Services.PageServices;
using Permissions.Services.PermissionServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS config
builder.Services.AddCors(o => o.AddPolicy("Policy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

//Database Context
builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//UnitOfWOrk
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Group
builder.Services.AddScoped<IGroupService, GroupService>();

//Page
builder.Services.AddScoped<IPageService, PageService>();

//Permissions
builder.Services.AddScoped<IPermissionService, PermissionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Policy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
