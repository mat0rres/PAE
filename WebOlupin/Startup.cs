using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Extensions;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;
using MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Extensions;
using MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Extensions;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Helpers;
using MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories;
using Serilog;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Framework.Guama.Media.Presentation.Web.Common.Options.RelativeMediaFileManagementOptions>(configuration.GetSection("FileManagement"));

            services.AddDbContext<Persistence.ORM.EF.ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, Persistence.ORM.EF.ApplicationDbContext>();

            services.PerformCoreWebSecurityCommonFrontendServiceRegistration();
            services.PerformSecurityPersonalWebBackendServiceRegistration();
            services.PerformMediaWebCommonRelativePathFileManagementServiceRegistration();

            services.AddScoped<IEntityRepository<Framework.Guama.Core.Persistence.ORM.EF.Entities.Trace, long>, Framework.Guama.Core.Persistence.ORM.EF.Repositories.TraceRepository>();
            services.AddScoped<IEntityRepository<Category, int>, CategoryRepository>();
            services.AddScoped<IEntityRepository<Brand, System.Guid>, BrandRepository>();
            services.AddScoped<IEntityRepository<BrandPromotion, System.Guid>, BrandPromotionRepository>();
            services.AddScoped<IEntityRepository<BrandSupportOffer, System.Guid>, BrandSupportOfferRepository>();
            services.AddScoped<IEntityRepository<RichTextBlock, RichTextBlockKind>, RichTextBlockRepository>();
            services.AddScoped<IEntityRepository<Post, long>, PostRepository>();
            services.AddScoped<IEntityRepository<SectionView, long>, SectionViewRepository>();
            services.AddScoped<IEntityRepository<Service, System.Guid>, ServiceRepository>();
            services.AddScoped<IEntityRepository<Product, System.Guid>, ProductRepository>();
            services.AddScoped<IEntityRepository<ShowcaseItem, System.Guid>, ShowcaseItemRepository>();
            services.AddScoped<IEntityRepository<Representative, System.Guid>, RepresentativeRepository>();
            services.AddScoped<IEntityRepository<CustomAttribute, System.Guid>, CustomAttributeRepository>();

            services.AddSingleton<Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.DbSchemaConfigurationProvider>();

            services.AddSingleton<Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper, DataInitializationHelper>();

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(config =>
            {
                config.AccessDeniedPath = new PathString("/error/403");
                config.LoginPath = new PathString("/security/account/login");
                config.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
                config.SlidingExpiration = true;
            });

            #region Security concreter dependencies configuration
            services.AddSingleton<Framework.Guama.Core.Presentation.Web.Security.Helpers.Facade.ILicenseValidationHelperFacade, WillcardLicenseValidationHelper>();

            services.AddTransient<Framework.Guama.Core.Presentation.Web.Security.Models.View.Contrat.IUserProfileViewModel, Models.View.UserProfileViewModel>();

            services.AddSingleton<Framework.Guama.Core.Presentation.Web.Common.ModelBinding.Contract.IConcretModelBindHelper, ConcretModelBindHelper>();

            services.AddScoped<Framework.Guama.Core.Presentation.Web.Security.Managers.Facade.IUserProfileManagerFacade, Managers.UserProfileManager>();

            services.AddSingleton<Framework.Guama.Core.Presentation.Web.Security.Providers.Facade.IRoleIdProviderFacade>(Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Providers.RoleMemoryProvider.Instance);
            #endregion

            #region Globalization configuration
            var defaultCulture = new System.Globalization.CultureInfo("es");

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
                options.SupportedCultures = new[] { defaultCulture };
                options.SupportedUICultures = new[] { defaultCulture };

                options.RequestCultureProviders = new[] { new Framework.Guama.Core.Presentation.Web.Common.Localization.UrlRequestCultureProvider() };
            });

            services.AddSingleton<GlobalViewLocalizationHelper>();
            #endregion

            services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => options.LowercaseUrls = true);

            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSession();

            services.AddMvc(mvc =>
            {
                mvc.EnableEndpointRouting = false;
                mvc.ModelBinderProviders.Insert(0, new Framework.Guama.Core.Presentation.Web.Common.ModelBinding.AbstractsModelBinderProvider());
            })
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(SharedResources));
                }
            );

            services.AddDataProtection().PersistKeysToFileSystem(new System.IO.DirectoryInfo(System.IO.Path.Combine(env.ContentRootPath, "dpkeys")));

            services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider, Microsoft.AspNetCore.Mvc.ViewFeatures.CookieTempDataProvider>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions> requestLocalizationOptions, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, DbContext dbContext)
        {
            loggerFactory.AddSerilog(Log.Logger);

            app.UseRequestLocalization(requestLocalizationOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsStaging())
            {
                dbContext.Database.Migrate();

                var dataInitializationHelpers = app.ApplicationServices.GetServices<Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper>();

                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var scopedDbContext = serviceScope.ServiceProvider.GetService<DbContext>();

                    foreach (var dataInitializationHelper in dataInitializationHelpers)
                    {
                        dataInitializationHelper.InitialiseData(scopedDbContext);
                    }
                }

                if (!((Persistence.ORM.EF.ApplicationDbContext)dbContext).Users.Any())
                {
                    var defaultUserId = "admin";

                    var k3y = new System.Guid(0x4c9294dd, 0x1e74, 0x45b2, 0xa8, 0x45, 0xaf, 0x6, 0xe5, 0x2a, 0xf6, 0x19);

                    var protectedPassword = (dbContext as Persistence.ORM.EF.ApplicationDbContext).CreateSecureUser(k3y, defaultUserId, "system");

                    Log.Logger.Information(string.Format(">>>>>> Protected Password : \"{0}\" => \"{1}\"", defaultUserId, protectedPassword));
                }
            }
            else
            {
                app.UseExceptionHandler("/error/exception");

                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            var atStartUpRoles = dbContext.Set<Framework.Guama.Core.Security.Common.Persistence.Entities.Role>().Include(e => e.PermissionBridges).ThenInclude(b => b.Stern).ToArray();

            foreach (var item in atStartUpRoles)
            {
                Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Providers.RoleMemoryProvider.Instance.Roles.Add(item);
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute("defaultAreaRoutes",
                    template: "{area:exists}/{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultRoutes",
                    template: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
