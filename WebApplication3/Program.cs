var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication("CookieAuthentication")
 .AddCookie("CookieAuthentication", config =>
 {
     config.Cookie.HttpOnly = true;
     config.Cookie.SecurePolicy = CookieSecurePolicy.None;
     config.Cookie.Name = "UserLoginCookie";
     config.LoginPath = "/Login/UserLogin";
     config.Cookie.SameSite = SameSiteMode.Strict;
 });
builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizePage("/Edit");
    options.Conventions.AuthorizePage("/Delete");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().AddSessionStateTempDataProvider();
    services.AddSession();
    services.AddRazorPages();
    services.AddHttpContextAccessor();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();
