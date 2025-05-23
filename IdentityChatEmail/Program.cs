using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmailContext>();

builder.Services.AddIdentity<AppUser, IdentityRole>().
    AddEntityFrameworkStores<EmailContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/UserLogin"; // ?? buraya giri? sayfan?z?n path�ini yaz?n
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
