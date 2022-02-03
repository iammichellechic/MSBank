using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MSBank.Data;
using MSBank.Models;
using MSBank.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BankAppDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddRoles<IdentityRole>() 
   .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddTransient<DataInitializer>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//builder.Services.AddResponseCaching();

builder.Services.AddRazorPages();

var app = builder.Build();

using(var scope=app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>().SeedData();

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseResponseCaching();

app.MapRazorPages();

app.Run();
