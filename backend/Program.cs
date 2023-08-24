using WebApi.Helpers;
using WebApi.Services;


var builder = WebApplication.CreateBuilder(args);

{
	var services = builder.Services;
	services.AddCors(); // Cross origin
	services.AddControllers(); // Add services required for controllers

    // Binds AppSettings.json to custom model, can be accessed like IOptions<AppSettings> appSettings.
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

	// Makes this service usable as a singleton across the application
	services.AddSingleton<IUserService, UserService>();
}

var app = builder.Build();

{
	app.UseCors(x => x
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());

	app.UseMiddleware<JwtMiddleware>();

	app.MapControllers(); // maps the routes to the controllers.
}

app.Run("http://localhost:4000");