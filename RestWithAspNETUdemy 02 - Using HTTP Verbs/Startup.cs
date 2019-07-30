using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestWithAspNETUdemy.Model.Context;
using RestWithAspNETUdemy.Business;
using RestWithAspNETUdemy.Business.Implementations;
using RestWithAspNETUdemy.Repository.Implementations;
using RestWithAspNETUdemy.Repository;
using RestWithAspNETUdemy.Repository.Generic;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using RestWithAspNETUdemy.Hypermedia;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using RestWithAspNETUdemy.Security.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace RestWithAspNETUdemy
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new[] { "db/migrations" },
                        IsEraseDisabled = true
                    };

                    evolve.Migrate();
                }
                catch (System.Exception ex)
                {
                    _logger.LogCritical("Database migration error", ex);
                    throw;
                }
            }

            var signingConfiguration = new SignInConfiguration();
            services.AddSingleton(signingConfiguration);

            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(_configuration.GetSection("TokenConfiguration"))
            .Configure(tokenConfiguration);
            services.AddSingleton(tokenConfiguration);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramValidation = bearerOptions.TokenValidationParameters;
                paramValidation.IssuerSigningKey = signingConfiguration.Key;
                paramValidation.ValidAudience = tokenConfiguration.Audience;
                paramValidation.ValidIssuer = tokenConfiguration.Issuer;

                paramValidation.ValidateIssuerSigningKey = true;
                paramValidation.ValidateLifetime = true;
                paramValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build()
                );
            });


            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.EnableEndpointRouting = false;
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RESTful API with ASP.NET Core", Version = "v1" });
            });

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);

            // Dependencies
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IBookBusiness, BookBusinessImpl>();
            services.AddScoped<ILoginBusiness, LoginBusinessImpl>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "DefaultApi", template: "{controller=Values}/{id?}");
            });
        }
    }
}
