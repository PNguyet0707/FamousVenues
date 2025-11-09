using AutoMapper;
using DataLayer.Extensions;
using FamousVenues.Extensions;
using Microsoft.Extensions.FileProviders;
using ServiceLayer.AutoMapper;
using ServiceLayer.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuth();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new VenueProfile()); 
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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
