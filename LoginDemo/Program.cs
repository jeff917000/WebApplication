using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                              .AddCookie(options =>
                              {
                                  // �H�U�o��ӳ]�w�i���i�L
                                  options.AccessDeniedPath = "/Home/AccessDeny";   // �ڵ��A�����\�n�J�A�|����o�@���C
                                  options.LoginPath = "/Home/Index";
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

#region LogingCookie 
app.UseAuthentication();
// ���ҡA�]�w HttpContext.User �ݩʡA�ðw��n�D������v�����n��C
#endregion

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
