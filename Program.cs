using AutoMapper;
using Danger_Money;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Danger_Money.Infra.Data;

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
    .UseSqlite("Data Source=Repository.db"));
builder.Services.AddDbContext<IdentityDbContext<ApplicationUser>>(options => options
    .UseSqlite("Data Source=Identity.db"));

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

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RepositoryDbContext>();
    await DataSeeder.SeedExpensesAsync(context);
}

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

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    


app.Run();
