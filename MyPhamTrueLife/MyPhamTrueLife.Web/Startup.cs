using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyPhamTrueLife.BLL.Implement;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL;
using MyPhamTrueLife.Web.Resources;
using System.Globalization;
using System.Reflection;

namespace MyPhamTrueLife.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mainConnectString = Configuration["MainConnectionString"];


            var supportedCultures = new[]
               {
                    new CultureInfo("en-US"),
                    new CultureInfo("vi-VN"),
                };
            //var credential = GoogleCredential.FromFile("config\\service_account.json");
            //FirebaseApp.Create(new AppOptions()
            //{

            //    Credential = credential
            //});
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(culture: "vi-VN", uiCulture: "vi-VN"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            options.RequestCultureProviders = new[]
            {
                 new RouteDataRequestCultureProvider() { Options = options }
            };

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .WithExposedHeaders("Content-Disposition");
                    });
            });
            services.AddSingleton(options);
            services.AddSingleton<LocService>();

            services.AddLocalization(otp => otp.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                 .AddDataAnnotationsLocalization(otp =>
                 {
                     otp.DataAnnotationLocalizerProvider = (type, factory) =>
                     {
                         var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                         return factory.Create("SharedResource", assemblyName.Name);
                     };
                 });

            services.AddControllers();
            //var inf1 = new OpenApiInfo
            //{
            //    Title = "API v1.0",
            //    Version = "v1",
            //    Description = "Swashbuckle",
            //    TermsOfService = new Uri("http://appointvn.com"),
            //    Contact = new OpenApiContact
            //    {
            //        Name = "Trang Nguyen",
            //        Email = "phuongtrang06@gmail.com"
            //    },
            //    License = new OpenApiLicense
            //    {
            //        Name = "Apache 2.0",
            //        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
            //    }
            //};
            //var inf2 = new OpenApiInfo
            //{
            //    Title = "API v2.0",
            //    Version = "v2",
            //    Description = "Swashbuckle",
            //    TermsOfService = new Uri("http://appointvn.com"),
            //    Contact = new OpenApiContact
            //    {
            //        Name = "Trang Nguyen",
            //        Email = "phuongtrang06@gmail.com"
            //    },
            //    License = new OpenApiLicense
            //    {
            //        Name = "Apache 2.0",
            //        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
            //    }
            //};
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", inf1);
                //c.SwaggerDoc("v2", inf2);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Pham True Life",
                    Version = "v1"
                });
                //var filePath = Path.Combine(AppContext.BaseDirectory, "TTL_API_5PTB_Version2.WebApi.xml");
                //c.IncludeXmlComments(filePath);

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);
            });
            // Config Services
            services.ConfigureDbContext(mainConnectString);
            services.AddScoped<IInfoTypeUserService, InfoTypeUserService>();
            services.AddScoped<IInfoUserService, InfoUserService>();
            services.AddScoped<IUserAdminService, UserAdminService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInfoComentService, InfoComentService>();
            services.AddScoped<IInfoEvaluateService, InfoEvaluateService>();
            services.AddScoped<IInfoCartService, InfoCartService>();
            services.AddScoped<IInfoServerService, InfoServerService>();
            services.AddScoped<IInfoProvinceService, InfoProvinceService>();
            services.AddScoped<IInfoDistrictService, InfoDistrictService>();
            services.AddScoped<IInfoAddressDeliveryUserService, InfoAddressDeliveryUserService>();
            services.AddScoped<IInfoOrderService, InfoOrderService>();
            services.AddScoped<IInfoTypeStaffService, InfoTypeStaffService>();
            services.AddScoped<IInfoStaffService, InfoStaffService>();
            services.AddScoped<IInfoCapicityService, InfoCapicityService>();
            services.AddScoped<IInfoTypeNatureService, InfoTypeNatureService>();
            services.AddScoped<IInfoCapicityProductService, InfoCapicityProductService>();
            services.AddScoped<IInfoNatureService, InfoNatureService>();
            services.AddScoped<IInfoPositionStaffService, InfoPositionStaffService>();
            services.AddScoped<IInfoPromotionService, InfoPromotionService>();
            services.AddScoped<IInfoProductPortfolioService, InfoProductPortfolioService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region -- Swagger --
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
            });
            #endregion
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
