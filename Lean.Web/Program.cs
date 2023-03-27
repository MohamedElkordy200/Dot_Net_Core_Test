using Lean.Web.Extensions;
using Lean.Web.MiddleWares;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddApplicationPart(typeof(Lean.Presentation.AssemblyReference).Assembly);

//Configure Services
builder.Services.ConfigureServices(builder.Configuration);
// Add services to the container.


var app = builder.Build();

//General Exceptions

app.UseMiddleware<ExceptionHandlingMiddleWare>();
//-------------Start localization------------------
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
//----------End localization-----------
// to make migration in the first time 
app.MigrateDatabase();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();