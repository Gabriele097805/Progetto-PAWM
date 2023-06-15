using Microsoft.AspNetCore.ResponseCompression;
using ToDoList;
using ToDoList.Authentication;
using ToDoList.Configuration;
using ToDoList.Database;
using ToDoList.Mappings;
using ToDoList.Services;

var builder = WebApplication.CreateBuilder(args);

// This adds the db context, the database configuration with which the queries and DB commands will be performed,
// and the migrations will be created with the commands in the readme
builder.Services.AddDbContext<AppDbContext>()
    .AddMigrationsRequiredDependencies();

// Add services to the container.
// Adding the dependency. The lifetime of the dependency is managed by the DI baked in the Framework.
builder.Services.AddScoped<IToDoListService, ToDoListService>();
builder.Services.AddScoped<IToDoListItemService, ToDoListItemService>();

builder.Services.AddOptions<GitHubConfiguration>()
    .BindConfiguration(nameof(GitHubConfiguration))
    .ValidateDataAnnotations();

builder.Services.AddControllersWithViews();

// Performance - adding static content compression
builder.Services.AddResponseCompression(options => 
{
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
    options.EnableForHttps = true;
});

builder.Services.AddHttpClient("GitHubOAuth", options =>
{
    options.BaseAddress = new Uri("https://github.com");
});

builder.Services.AddGitHubOAuth(builder.Configuration);

builder.Services.AddAutoMapper(config => config.AddProfile<DbProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.ApplyMigrations();

app.UseMiddleware<ExceptionHandler>();  
app.UseResponseCompression();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "/api/{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html")
    .RequireAuthorization();

app.Run();
