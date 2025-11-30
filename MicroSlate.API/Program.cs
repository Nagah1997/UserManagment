using MicroSlate.API.Extension;
using MicroSlate.Application;
using MicroSlate.Infrastructure;
using MicroSlate.Infrastructure.Identity;
using MicroSlate.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDiServices(builder.Configuration);

// Add Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServicesLayer(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        var settings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.Issuer,
            ValidAudience = settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(settings.Key))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Seed Roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await IdentitySeeder.SeedRoles(roleManager);
    await UserSeeder.SeedAsync(userManager);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
