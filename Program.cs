using AutoMapper;
using Danger_Money;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext<ApplicationUser>>();

builder.Services.AddAuthorization();



//DB
builder.Services.AddDbContext<RepositoryDbContext>(options => options
    .UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:RepositoryDB").Value));
builder.Services.AddDbContext<IdentityDbContext<ApplicationUser>>(options => options
    .UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:IdentityDB").Value));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mapper
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Expense, ExpenseDTO>().ReverseMap();
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


//________________________________________________________
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
