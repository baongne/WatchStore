using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EasyBuy_Backend.Services;
using EasyBuy_Backend.Services.AuthSvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);


// **Bước 1: Thêm dịch vụ CORS**
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins", builder =>
	{
		builder
			.AllowAnyOrigin() // Cho phép tất cả các nguồn
			.AllowAnyMethod() // Cho phép tất cả các phương thức
			.AllowAnyHeader(); // Cho phép tất cả các header
	});
});

// Add services to the container.
builder.Services.AddServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký MyDbContext với Dependency Injection và cấu hình kết nối đến cơ sở dữ liệu SQL Server
builder.Services.AddDbContext<MyDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRepositories();

builder.Services.AddScoped<IAuthService, AuthService>();

// Thêm các dịch vụ như Authentication và JWT Bearer
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidateAudience = true,
		ValidAudience = builder.Configuration["JWT:Audience"],
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(
			System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
		),
		ValidateLifetime = true
	};
});

builder.Services.AddAuthorization(options => {
	options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
	options.AddPolicy("Customer", policy => policy.RequireClaim("role", "customer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// **Bước 2: Áp dụng chính sách CORS**
app.UseHttpsRedirection();
// Thêm dòng này để áp dụng chính sách CORS
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
