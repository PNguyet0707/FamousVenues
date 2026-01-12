using DataLayer.Extensions;
using DataLayer.UnitOfWorks.Implementations;
using DataLayer.UnitOfWorks.Interfaces;
using FamousVenues.ExceptionHandler;
using FamousVenues.Extensions;
using Microsoft.Extensions.FileProviders;
using ServiceLayer.AutoMapper;
using ServiceLayer.ExceptionHandler;
using ServiceLayer.Implementations;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ConnectionExceptionHandler>();
builder.Services.AddExceptionHandler<DatabaseExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuth();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IChefService, ChefService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new VenueProfile());
    cfg.AddProfile(new DishProfile());
    cfg.AddProfile(new ChefProfile());
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Fe port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await AdminHelper.CreateAdminDefaultAsync(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var imagePath = Path.Combine(builder.Environment.ContentRootPath, "VenueImages");
if (!Directory.Exists(imagePath))
{
    Directory.CreateDirectory(imagePath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "VenueImages")),
    RequestPath = "/VenueImages"
});
//app.UseExceptionHandler(options =>
//{
//    options.Run(async context =>
//    {
//        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
//        context.Response.ContentType = "application/json";      
//        var exception = context.Features.Get<IExceptionHandlerFeature>();
//        if (exception != null)
//        {
//            var message = $"{exception.Error.Message}";
//            var payload = new { error = message };
//            var json = JsonSerializer.Serialize(payload);
//            await context.Response.WriteAsync(json).ConfigureAwait(false);
//        }
//    });
//});
//app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseExceptionHandler("/Error");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
