using EXE_Dotel.Models;
using EXE_Dotel.Repository.Rental;
using EXE_Dotel.Repository.RentalImages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DotelDBContext>(opt=> opt.UseSqlServer("server =localhost; database = DotelDB;uid=khoand;pwd=1234; trusted_connection = true; "));

builder.Services.AddScoped<IRentalRepository, RentalRepostiory>();
builder.Services.AddScoped<iRentalImageRepository, RentalImageRepository>();
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
