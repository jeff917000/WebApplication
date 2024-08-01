var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                              .AddCookie(options =>
                              {
                                  options.AccessDeniedPath = "/AccessAccount/AccessDeny";
                                  options.LoginPath = "/Home/Index";
                              });

builder.Services.AddSession(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;    // 限制只有在 HTTPS 連線的情況下，才允許使用 Session。
    //options.Cookie.Name = "";                                 // 預設 Session 名稱 .AspNetCore.Session 可以改掉。
    options.IdleTimeout = TimeSpan.FromMinutes(20);             // 預設是 20分鐘，可修改
});

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

app.UseAuthentication();    // 驗證
app.UseAuthorization();     // 授權
app.UseSession();           // Session

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
