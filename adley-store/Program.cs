using adley_store.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AdleyStoreConnectionString");
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "adleyStore.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(300); // Set the session timeout duration
    options.Cookie.HttpOnly = true;
    // Add more session options as needed
});
builder.Services.AddDbContextPool<AdleyDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("AdleyStoreConnectionString"), ServerVersion.AutoDetect(connectionString)));
    //UseSqlServer(
    //    builder.Configuration.GetConnectionString("AdleyStoreConnectionString")));


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
// dotnet ef migrations add InitialCreate --context ApplicationDbContext
// dotnet ef database update -c ApplicationDbContext
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

