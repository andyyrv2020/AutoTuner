using AutoTuner.Data;
using AutoTuner.Services;
using AutoTuner.ViewModels;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IRecommendationService, RecommendationService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHangfire(configuration =>
{
    configuration.UseMemoryStorage();
});

builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

RecurringJob.AddOrUpdate<IRecommendationService>("refresh-demo-car", service => service.GenerateRecommendationsAsync(1, new RecommendationRequestOptions()), Cron.Daily);

app.Run();
