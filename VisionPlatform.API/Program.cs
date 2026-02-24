using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VisionPlatform.API.Authorization;
using VisionPlatform.Application.DTOs.Versions;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Application.Services;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Auth;
using VisionPlatform.Infrastructure.Data;
using VisionPlatform.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ===============================
// Controllers + Swagger
// ===============================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "VisionPlatform.API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Digite: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// ===============================
// Injeção de Dependências
// ===============================

// Auth
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IJwtService, JwtService>();

//Users
builder.Services.AddScoped<IUserService, UserService>();

// Version
builder.Services.AddScoped<IVersionRepository, VersionRepository>();
builder.Services.AddScoped<IVersionService, VersionService>();

// VersionTask
builder.Services.AddScoped<IVersionTaskRepository, VersionTaskRepository>();
builder.Services.AddScoped<IVersionTaskService, VersionTaskService>();

// Cliente
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

// Área
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IAreaService, AreaService>();

// Permissões
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

// Dasboard
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();


// ===============================
// DbContext
// ===============================

builder.Services.AddDbContext<VisionDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

// ===============================
// JWT Authentication
// ===============================

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ===============================
// Authorization Policies
// ===============================

builder.Services.AddAuthorization(options =>
{
    // VERSION
    options.AddPolicy("Version.Create",
        policy => policy.Requirements.Add(new PermissionRequirement("Version.Create")));

    options.AddPolicy("Version.Delete",
        policy => policy.Requirements.Add(new PermissionRequirement("Version.Delete")));

    options.AddPolicy("Version.Release",
        policy => policy.Requirements.Add(new PermissionRequirement("Version.Release")));

    // VERSION TASK
    options.AddPolicy("VersionTask.Create",
        policy => policy.Requirements.Add(new PermissionRequirement("VersionTask.Create")));

    options.AddPolicy("VersionTask.Update",
        policy => policy.Requirements.Add(new PermissionRequirement("VersionTask.Update")));

    options.AddPolicy("VersionTask.Delete",
        policy => policy.Requirements.Add(new PermissionRequirement("VersionTask.Delete")));

    options.AddPolicy("VersionTask.AssignQA",
        policy => policy.Requirements.Add(new PermissionRequirement("VersionTask.AssignQA")));

    options.AddPolicy("VersionTask.MarkMerge",
        policy => policy.Requirements.Add(new PermissionRequirement("VersionTask.MarkMerge")));

    // USERS
    options.AddPolicy("User.View",
        policy => policy.Requirements.Add(new PermissionRequirement("User.View")));

    options.AddPolicy("User.Create",
        policy => policy.Requirements.Add(new PermissionRequirement("User.Create")));

    options.AddPolicy("User.Update",
        policy => policy.Requirements.Add(new PermissionRequirement("User.Update")));

    options.AddPolicy("User.Delete",
        policy => policy.Requirements.Add(new PermissionRequirement("User.Delete")));
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<VisionDbContext>();

    await DatabaseSeeder.SeedAsync(context);
}

// ===============================
// Pipeline
// ===============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();   // ⚠️ Sempre antes
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VisionDbContext>();
    await DatabaseSeeder.SeedAsync(context);
}

app.Run();
