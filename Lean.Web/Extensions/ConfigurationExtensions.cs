using System.Globalization;
using System.Net.Http;
using Lean.Contracts.MessageModels;
using Lean.Domain.DBEntities.Administration;
using Lean.Domain.Repositories;
using Lean.Persistence;
using Lean.Persistence.Mapper;
using Lean.Persistence.Repositories;
using Lean.Services;
using Lean.Services.Abstractions;
using Lean.Web.Filters.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lean.Web.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //add httpContext to use in down service
            services.AddHttpContextAccessor();
            

            //Database
            var connectionString = configuration.GetConnectionString("ApplicationContext");
            services.AddDbContext<ApplicationDbContext>((options) =>
            {
                options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly("Lean.Persistence"));
                //Logger for query
                options.UseLoggerFactory(
                    LoggerFactory.Create(builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Information);
                        builder.AddDebug();
                    })
                );
            });
            //configure Identity
            services.AddIdentity<User, IdentityRole<Guid>>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequiredLength = 1;

            }).AddEntityFrameworkStores<ApplicationDbContext>();
            //Auto mapper
            services.AddAutoMapper(typeof(ApplicationMapperProfiler));
            //Authentication
            services.AddAuthentication();

            //configure bind data model
            services.AddMvc()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var modelState = actionContext.ModelState.Values;
                        return new BadRequestObjectResult(
                            new ErrorMessage(
                                modelState.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage.ToString() ??
                                "Error happened When Bind Data"));
                    };
                });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            ;
            services.AddEndpointsApiExplorer();
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "(Onion architecture And  Identity for Authentication ) With Simple Crud Operations"
                });

                c.OperationFilter<AddRequiredHeaderParameter>();
            });

            // configure service for localization
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new("en"),
                        new("ar"),
                    };
                    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    var firstLang = userLangs.Split(',').FirstOrDefault();
                    var culture = string.IsNullOrEmpty(firstLang)
                        ? configuration.GetSection("Globalization:culture").ToString()
                        : firstLang;
                    var uiCulture = string.IsNullOrEmpty(firstLang)
                        ? configuration.GetSection("Globalization:uiCulture").ToString()
                        : firstLang;
                    options.DefaultRequestCulture = new RequestCulture(culture: "ar", uiCulture: "ar");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    return await Task.FromResult(new ProviderCultureResult(culture, uiCulture));
                }));
            });


            //custom Authorize
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 404;

                    context.Response.WriteAsync(
                        JsonConvert.SerializeObject(new ErrorMessage(RM.Common.YouShouldLoginFirst)));
                    return Task.CompletedTask;
                };
            });

            //Injects services
            services.AddScoped<IServicesManager, ServicesManager>();

            services.AddScoped<IRepositoriesManager, RepositoriesManager>();
        }
    }
}