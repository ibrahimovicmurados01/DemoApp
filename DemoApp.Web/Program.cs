using DemoApp.Entities;
using DemoApp.Web.Extensions;
using DemoApp.Web.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddDbContext<RepositoryContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {

        option.MigrationsAssembly(Assembly.GetAssembly(typeof(RepositoryContext)).GetName().Name);
    });

    x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddRepository();

//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Day) // Set the file path and rolling interval
            .CreateLogger();

app.UseSerilogRequestLogging();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

