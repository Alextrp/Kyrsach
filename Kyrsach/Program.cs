using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer;
using DAL;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using BLL.DTO;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DAL.IRepositories;
using DAL.Repositories;
using BLL.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DAL.AppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DAL.AppContext>()
    .AddDefaultTokenProviders();





builder.Services.AddScoped<SignInManager<User>>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Добавляем схему для cookie аутентификации
})
.AddCookie(options =>
{
    options.Cookie.Name = "token"; // Название cookie, где будет храниться токен
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Время жизни cookie
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            //builder.WithOrigins("https://localhost:7134", "https://localhost:7095")
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            //.AllowCredentials();
        });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Клиент", policy => policy.RequireRole("Клиент"));
    options.AddPolicy("Администратор", policy => policy.RequireRole("Администратор"));
    options.AddPolicy("Менеджер", policy => policy.RequireRole("Менеджер"));
    options.AddPolicy("Водитель", policy => policy.RequireRole("Водитель"));
});

var serviceProvider = builder.Services.BuildServiceProvider();

var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
string[] roleNames = { "Клиент", "Администратор", "Менеджер", "Водитель" };
IdentityResult roleResult;
foreach (var roleName in roleNames)
{
    var roleExist = await roleManager.RoleExistsAsync(roleName);
    if (!roleExist)
    {
        // Создать роль, если она не существует
        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
    }
}

//builder.Services.AddScoped<ICargoRepository, CargoRepository>();
//builder.Services.AddScoped<ICargoTypeRepository, CargoTypeRepository>();
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IService<CargoDTO>, CargoService>();
builder.Services.AddScoped<IService<CargoTypeDTO>, CargoTypeService>();
builder.Services.AddScoped<IService<OrderStatusDTO>, OrderStatusService>();
builder.Services.AddScoped<IService<PaymentDTO>, PaymentService>();
builder.Services.AddScoped<IService<ReviewDTO>, ReviewService>();
builder.Services.AddScoped<IService<SessionDTO>, SessionService>();
builder.Services.AddScoped<IService<TrackingDTO>, TrackingService>();
builder.Services.AddScoped<IService<UserDTO>, UserService>();
builder.Services.AddScoped<IService<VehicleDTO>, VehicleService>();
builder.Services.AddScoped<IService<VehicleTypeDTO>, VehicleTypeService>();

builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // добавляем сервисы сессии

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession();

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
