using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BuildingManagement.Infrastructure.Data.Configurations;
using BuildingManagement.Application.Common;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Infrastructure.Data;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Application.Services;
using BuildingManagement.Application.Mappings;
using BuildingManagement.Infrastructure.Security;
using BuildingManagement.Domain.Ultility;
using BuildingManagement.Application.Interfaces.Services.Ultility;
using BuildingManagement.Application.Services.Ultility;
using BuildingManagement.API.Filter;
using System.Security.Claims;
using BuildingManagement.Application.Chat;
using OfficeOpenXml;
using BuildingManagement.Application.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);
ExcelPackage.License.SetNonCommercialPersonal("Lăng Thảo Văn"); ; // Set your commercial license key here
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// add EmailSetting
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Smtp"));
builder.Services.Configure<OTPConfiguration>(builder.Configuration.GetSection("Otp"));

// add authentication

var jwtSetting = builder.Configuration.GetSection("JwtSettings").Get<JwtConfiguration>();
builder.Services.AddSingleton(jwtSetting);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSetting.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSetting.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret)),

        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.NameIdentifier
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            // Nếu request đến SignalR hub và có token trong query string
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chatHub"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// inject services 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IToaNhaServices, ToaNhaServices>();
builder.Services.AddScoped<IKhoiNhaService, KhoiNhaService>();
builder.Services.AddScoped<ITangLauServices, TangLauServices>();
builder.Services.AddScoped<ILoaiMatBangService, LoaiMatBangService>();
builder.Services.AddScoped<ITrangThaiMatBangService, TrangThaiMatBangService>();
builder.Services.AddScoped<IMatBangService, MatBangService>();
builder.Services.AddScoped<ILoaiDichVuService, LoaiDichVuService>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<INKBTLichSuBaoTriService, NKBTLichSuBaoTriService>();
builder.Services.AddScoped<INKBTHeThongSerivce, NKBTHeThongService>();
builder.Services.AddScoped<INKBTKeHoachBaoTriService, NKBTKeHoachBaoTriService>();
builder.Services.AddScoped<INKBTChiTietBaoTriService, NKBTChiTietBaoTriService>();
builder.Services.AddScoped<IKhacHangService, KhachHangService>();
builder.Services.AddScoped<INKBTTrangThaiBaoTriService, NKBTTrangThaiBaoTriService>();
builder.Services.AddScoped<IPhieuThuService, PhieuThuService>();
builder.Services.AddScoped<IDichVuNuocDinhMucService, DichVuNuocDinhMucService>();
builder.Services.AddScoped<IDichVuNuocService, DichVuNuocService>();
builder.Services.AddScoped<IDichVuDienDinhMucService, DichVuDienDinhMucService>();
builder.Services.AddScoped<IDichVuDienService, DichVuDienService>();
builder.Services.AddScoped<IDichVuNuocDongHoService, DichVuNuocDongHoService>();
builder.Services.AddScoped<IDichVuDienDongHoService, DichVuDienDongHoSerivce>();
builder.Services.AddScoped<IDichVuGuiXeLoaiXeService, DichVuGuiXeLoaiXeService>();
builder.Services.AddScoped<IDichVuGuiXeTheXeService, DichVuGuiXeTheXeService>();
builder.Services.AddScoped<IDichVuHoaDonService, DichVuHoaDonService>();
builder.Services.AddScoped<IDichVuSuDungSerivce, DichVuSuDungService>();
builder.Services.AddScoped<IPhongBanService, PhongBanService>();
builder.Services.AddScoped<IDichVuService, DichVuService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IYeuCauBaoTriService, YeuCauBaoTriService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<PaymentNotificationBackgroundService>();
// ultility service
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IOTPService, OTPService>();


// add performance filter
builder.Services.AddScoped<ApiPerformanceFilter>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
// add JWT service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();


// add signalR
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
});

builder.Services.AddHttpClient();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// add swgger authen
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Building Management API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Put your token here",
        Name = "Authentication",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder
            .WithOrigins(
                "http://localhost:3000",
                "https://localhost:3000",
                "http://localhost:3001",
                "https://localhost:3001"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateBuilding", policy =>
    {
        policy.RequireClaim("permissions", "building.create");
    });

    options.AddPolicy("UpdateBuilding", policy =>
    {
        policy.RequireClaim("permissions", "building.update");
    });
    options.AddPolicy("DeleteBuilding", policy =>
    {
        policy.RequireClaim("permissions", "building.delete");
    });
    options.AddPolicy("ViewBuilding", policy =>
    {
        policy.RequireClaim("permissions", "building.view");
    });
});



// add default database connection string
builder.Services.AddDbContext<BuildingManagementDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
// Configure the HTTP request pipeline.
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chatHub");
});


app.Run();
