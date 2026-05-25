using ISFDyT124.Data;
using ISFDyT124.DTOs;
using Microsoft.EntityFrameworkCore; // Importa Entity Framework Core para acceso a base de datos 
//using ISFDyT124.DTOs; // Importa objetos de transferencia de datos 

var builder = WebApplication.CreateBuilder(args); // Crea el constructor del builder de la aplicación web 

// Configura la conexión a la base de datos SQL Server usando el contexto SiAsContext 
builder.Services.AddDbContext<SiAsContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("DBSI") ??
        throw new InvalidOperationException("Connection string 'DBSI' not found.")));

//Añade el servicio UsuarioDTO con duración por alcance (scoped) 
builder.Services.AddScoped<UsuarioDTO>();

// Añade controladores con vistas para MVC 
builder.Services.AddControllersWithViews();

// Configura la autenticación basada en cookies 
builder.Services.AddAuthentication("Cookies") // Define el esquema de autenticación llamado "Cookies" 
    .AddCookie("Cookies", options => // Configura opciones para autenticación por cookies 
    {
        options.LoginPath = "/Access/Login"; // Ruta a la página de login para redirección en caso de no autenticado 
        options.LogoutPath = "/Access/Salir"; // Ruta para cerrar sesión
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiración de la cookie (30 minutos) 
        options.SlidingExpiration = true; // Renueva el tiempo de expiración al solicitar recursos si el usuario está activo 
        options.AccessDeniedPath = "/Home/Privacy"; // Ruta a la que redirige si el usuario no tiene permisos 
    });

var app = builder.Build(); // Construye la aplicación con la configuración realizada 

// Configuraciones para ambientes que NO son de desarrollo 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Manejo global de excepciones, lleva a la página de error 
    app.UseHsts(); // Usa HTTP Strict Transport Security para proteger la app en producción 
}
//Middleware
app.UseHttpsRedirection(); // Redirige solicitudes HTTP a HTTPS 
app.UseStaticFiles(); // Habilita servir archivos estáticos (CSS, JS, imágenes) 
app.UseRouting(); // Habilita el enrutamiento de solicitudes HTTP 
app.UseAuthentication(); // Habilita la autenticación en middleware para validar usuarios 
app.UseAuthorization(); // Habilita autorización para acceso a recursos // Define la ruta por defecto para las peticiones MVC: controlador, acción y parámetro opcional id 
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run(); // Ejecuta la aplicación web
