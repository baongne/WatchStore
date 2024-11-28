using EasyBuy_Frontend_Admin.RouteConfigs;
using EasyBuy_Frontend_Admin.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(15);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient();

builder.Services.AddServices();

var app = builder.Build();

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

app.UseSession();

app.UseAuthorization();

app.Use(async (context, next) =>
{
	// Kiểm tra nếu người dùng đã đăng nhập (giả sử thông tin lưu trong session)
	var isAuthenticated = context.Session.GetString("IsAuthenticated");

    if (string.IsNullOrEmpty(isAuthenticated) &&
       context.Request.Path != "/Auth/SignIn" &&
       context.Request.Path != "/Auth/SignUp")
    {
        // Chuyển hướng tới trang đăng nhập
        context.Response.Redirect("/Auth/SignIn");
        return;
    }

    // Nếu đã đăng nhập hoặc đang ở trang đăng nhập, tiếp tục yêu cầu
    await next();
});

RouteConfigManager.RegisterAllRoutes(app);

app.Run();
