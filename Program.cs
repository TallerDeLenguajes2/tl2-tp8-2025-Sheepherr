using Microsoft.AspNetCore.Http;
using MVC.Interfaces;
using MVC.Repositorios;
using MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Servicios de Sesión y Acceso a Contexto (CLAVE para la autenticación)
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
 options.IdleTimeout = TimeSpan.FromMinutes(30);
 options.Cookie.HttpOnly = true;
 options.Cookie.IsEssential = true;
});
// Registro de la Inyección de Dependencia (TODOS AddScoped)
builder.Services.AddScoped<IProductosRepository, ProductosRepository>();
builder.Services.AddScoped<IPresupuestosRepository, PresupuestosRepository>();
builder.Services.AddScoped<IUserRepository, UsuarioRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
