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
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;    // ����u���b HTTPS �s�u�����p�U�A�~���\�ϥ� Session�C
    //options.Cookie.Name = "";                                 // �w�] Session �W�� .AspNetCore.Session �i�H�ﱼ�C
    options.IdleTimeout = TimeSpan.FromMinutes(20);             // �w�]�O 20�����A�i�ק�
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

app.UseAuthentication();    // ����
app.UseAuthorization();     // ���v
app.UseSession();           // Session

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
