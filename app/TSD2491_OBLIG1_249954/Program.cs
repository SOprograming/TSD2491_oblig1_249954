using Microsoft.EntityFrameworkCore;
using TSD2491_OBLIG1_249954.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BrukerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BrukerContext") ?? throw new InvalidOperationException("Connection string 'BrukerContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
