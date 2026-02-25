using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using VisionPlatform.API.Authorization;
using VisionPlatform.API.Middleware;
using VisionPlatform.Application.DTOs.Versions;
using VisionPlatform.Application.Interfaces;
using VisionPlatform.Application.Services;
using VisionPlatform.Domain.Interfaces;
using VisionPlatform.Infrastructure.Auth;
using VisionPlatform.Infrastructure.Data;
using VisionPlatform.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// Configurações de Base
// ===============================
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ===============================
// Controllers + JSON Enums
// ===============================
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Faz com que os Enums apareçam como Texto no Swagger e no Angular
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

// ===============================
// CORS - Permissão para o Angular
// ===============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ===============================
// Swagger com Suporte a JWT
// ===============================
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
// Injeção de Dependências (DI)
// ===============================

// Auth & Infra
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Domain Services & Repositories
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVersionRepository, VersionRepository>();
builder.Services.AddScoped<IVersionService, VersionService>();
builder.Services.AddScoped<IVersionTaskRepository, VersionTaskRepository>();
builder.Services.AddScoped<IVersionTaskService, VersionTaskService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// Permissões e Segurança
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

// ===============================
// Banco de Dados (MySQL)
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
    // Version Policies
    options.AddPolicy("Version.Create", p => p.Requirements.Add(new PermissionRequirement("Version.Create")));
    options.AddPolicy("Version.Delete", p => p.Requirements.Add(new PermissionRequirement("Version.Delete")));
    options.AddPolicy("Version.Release", p => p.Requirements.Add(new PermissionRequirement("Version.Release")));

    // Task Policies
    options.AddPolicy("VersionTask.Create", p => p.Requirements.Add(new PermissionRequirement("VersionTask.Create")));
    options.AddPolicy("VersionTask.Update", p => p.Requirements.Add(new PermissionRequirement("VersionTask.Update")));
    options.AddPolicy("VersionTask.Delete", p => p.Requirements.Add(new PermissionRequirement("VersionTask.Delete")));
    options.AddPolicy("VersionTask.AssignQA", p => p.Requirements.Add(new PermissionRequirement("VersionTask.AssignQA")));
    options.AddPolicy("VersionTask.MarkMerge", p => p.Requirements.Add(new PermissionRequirement("VersionTask.MarkMerge")));

    // User Policies
    options.AddPolicy("User.View", p => p.Requirements.Add(new PermissionRequirement("User.View")));
    options.AddPolicy("User.Create", p => p.Requirements.Add(new PermissionRequirement("User.Create")));
    options.AddPolicy("User.Update", p => p.Requirements.Add(new PermissionRequirement("User.Update")));
    options.AddPolicy("User.Delete", p => p.Requirements.Add(new PermissionRequirement("User.Delete")));
});

var app = builder.Build();

// ===============================
// Pipeline de Execução (Middleware)
// ===============================

// 1. Tratamento Global de Erros (Sempre primeiro)
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. CORS (Deve vir ANTES de Auth)
app.UseCors("AngularApp");

// 3. Segurança
app.UseAuthentication();
app.UseAuthorization();

// 4. Endpoints
app.MapControllers();

// ===============================
// Inicialização de Dados (Seed)
// ===============================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<VisionDbContext>();
    // O Seed roda apenas uma vez na inicialização
    await DatabaseSeeder.SeedAsync(context);
}

app.Run();