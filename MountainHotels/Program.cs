using Microsoft.EntityFrameworkCore;
using MountainHotels.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MountainHotelsContext>(
    options =>
        options
            .UseNpgsql(
                builder.Configuration["ConnectionStrings:DefaultConnection"]
                    ?? throw new InvalidOperationException(
                        "Connection string 'MountainHotelsDb' not found."
                    )
            )
            .UseSnakeCaseNamingConvention()
);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MountainHotelsContext>();
    dbContext.Database.Migrate();
}

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

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
