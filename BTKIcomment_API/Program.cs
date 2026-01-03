using BTKECommerce_core.DTOs.Category;
using BTKECommerce_core.Maper;
using BTKECommerce_core.Services.Abstract;
using BTKECommerce_core.Services.Concrate;
using BTKECommerce_Core.Services.Concrete;
using BTKECommerce_domain.Data;
using BTKECommerce_domain.Entities;
using BTKECommerce_Domain.Interfaces;
using BTKECommerce_Infrastructure.Extensions.Token;
using BTKECommerce_Infrastructure.Models;
using BTKECommerce_Infrastructure.Repository;
using BTKECommerce_Infrastructure.UoW;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(BaseResponseModel<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryDTO>();

#region Db Connection
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion


#region Identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
#endregion



#region Jwt Header Configuration

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

#endregion









#region JWT Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion




#region DI AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
#endregion

#region DI
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
#endregion


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var adminEmail = "admin@btkcommerce.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdminUser = new ApplicationUser
        {
            UserName = "admin",
            Email = adminEmail,
            FirstName = "System",
            LastName = "Administrator"
        };

        var createAdminResult = await userManager.CreateAsync(newAdminUser, "Admin123!");
        if (createAdminResult.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdminUser, "Admin");
        }

    }
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();