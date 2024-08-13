// <copyright file="Startup.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity
{
  using System.Globalization;
  using System.Reflection;
  using FluentValidation;
  using FluentValidation.AspNetCore;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Localization;
  using Microsoft.EntityFrameworkCore;

  /// <summary>
  /// Provides the startup type used by the <see cref="IWebHostBuilder"/>.
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/>.</param>
    /// <remarks>
    /// This is called automatically to configure the services.
    /// </remarks>
    public void ConfigureServices(IServiceCollection services)
    {
      var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      if (string.IsNullOrWhiteSpace(environment))
      {
        environment = "Development";
      }

      var configuration =
        new ConfigurationBuilder()
          .AddJsonFile(
            "AppSettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile(
            $"AppSettings.{environment}.json",
            optional: true,
            reloadOnChange: true)
          .Build();

      services.AddLocalization();
      services.AddMvc();

      services
        .AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssemblyContaining(typeof(Startup));

      services.AddDbContext<DefaultIdentityDbContext>(
        options =>
        {
          options.UseSqlServer(
            configuration.GetConnectionString("IdentityConfigurationData"));
        });

      services
        .AddIdentity<DefaultIdentityUser, IdentityRole>(
          options =>
          {
            options.User.AllowedUserNameCharacters = string.Empty;
            options.ClaimsIdentity.SecurityStampClaimType = "security_stamp";
          })
        .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
        .AddEntityFrameworkStores<DefaultIdentityDbContext>()
        .AddDefaultTokenProviders();

      // Define the migration assembly for identity
      // server contexts.
      var migrationAssembly =
        typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

      services
        .AddIdentityServer(
          options =>
          {
            options.UserInteraction.LoginUrl = "/SignIn";
            options.UserInteraction.LogoutUrl = "/SignOut";
            options.UserInteraction.ErrorUrl = "/Error";

            options.KeyManagement.Enabled = false;
          })
        .AddConfigurationStore(
          options =>
          {
            options.ConfigureDbContext = builder =>
            {
              builder.UseSqlServer(
                configuration.GetConnectionString(
                  "IdentityConfigurationData"),
                options => options.MigrationsAssembly(migrationAssembly));
            };
          })
        .AddConfigurationStoreCache()
        .AddOperationalStore(
          options =>
          {
            options.ConfigureDbContext = builder =>
            {
              builder.UseSqlServer(
                configuration.GetConnectionString(
                  "IdentityConfigurationData"),
                options => options.MigrationsAssembly(migrationAssembly));
            };

            options.EnableTokenCleanup = true;
            options.RemoveConsumedTokens = true;
          })
        .AddAspNetIdentity<DefaultIdentityUser>();

      services.AddAuthentication();

      services.ConfigureApplicationCookie(
        options =>
        {
          options.LoginPath = "/SignIn";
        });
    }

    /// <summary>
    /// Configures the application.
    /// </summary>
    /// <param name="application">A <see cref="IApplicationBuilder"/>.</param>
    /// <param name="environment">A <see cref="IWebHostEnvironment"/>.</param>
    /// <remarks>
    /// This is called automatically to create the request processing pipeline.
    /// </remarks>
    public void Configure(
      IApplicationBuilder application, IWebHostEnvironment environment)
    {
      if (environment.IsDevelopment())
      {
        application.UseDeveloperExceptionPage();
      }

      var supportedCultures =
        new CultureInfo[]
        {
          new CultureInfo("en-GB"),
        };

      application.UseRequestLocalization(
        new RequestLocalizationOptions
        {
          SupportedCultures = supportedCultures,
          SupportedUICultures = supportedCultures,

          DefaultRequestCulture = new RequestCulture("en-GB"),
        });

      application.UseStaticFiles();
      application.UseRouting();
      application.UseIdentityServer();
      application.UseAuthorization();

      application.UseEndpoints(
        endpoints =>
        {
          endpoints.MapControllers().RequireAuthorization();
        });
    }
  }
}
