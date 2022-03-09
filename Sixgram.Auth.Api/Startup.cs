using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Sixgram.Auth.Core.Authentication;
using Sixgram.Auth.Core.Hashing;
using Sixgram.Auth.Core.Profiles;
using Sixgram.Auth.Core.Services;
using Sixgram.Auth.Core.Token;
using Sixgram.Auth.Database;
using Sixgram.Auth.Database.Repository.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sixgram.Auth.Core.Authentication.RestoringPassword;
using Sixgram.Auth.Core.Http;
using Sixgram.Auth.Core.Options;
using Sixgram.Auth.Core.User;
using Sixgram.Auth.Database.Repository.RestoringCode;

namespace Sixgram.Auth.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure App Options
            services.Configure<AppOptions>(Configuration.GetSection(AppOptions.App));
            var appOptions = Configuration.GetSection(AppOptions.App).Get<AppOptions>();
            services.AddSingleton(appOptions);

            // Configure SmtpClient Options
            services.Configure<SmtpClientOptions>(Configuration.GetSection(SmtpClientOptions.SmtpClient));
            var smtpClientOptions = Configuration.GetSection(SmtpClientOptions.SmtpClient).Get<SmtpClientOptions>();
            services.AddSingleton(smtpClientOptions);

            // Configure Nlog Options
            services.Configure<NLogConfigOptions>(Configuration.GetSection(NLogConfigOptions.Nlog));
            var nlogOptions = Configuration.GetSection(NLogConfigOptions.Nlog).Get<NLogConfigOptions>();
            services.AddSingleton(nlogOptions);

            // Configure Authentication
            ConfigureAuthentication(services);

            // Configure Swagger
            ConfigureSwagger(services);

            // Configure Repositories & Services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRestoringCodeRepository, RestoringCodeRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRestorePasswordService, RestorePasswordService>();
            services.AddScoped<IUserService, UserService>();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(connection));
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection,
                x => x.MigrationsAssembly("Sixgram.Auth.Database")));

            //Configure AutoMapper Profile
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new AppProfile()); });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddControllers();

            //Configure HttpClient
            services.AddHttpClient("posts", p =>
            {
                p.BaseAddress = new Uri("http://localhost:5184/");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    opt =>
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            opt.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                    });
            }
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration["AppOptions:SecretKey"]);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuer = false,
                        RequireExpirationTime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                    x.SaveToken = true;
                });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "Bearer {authToken}",
                    Description = "JSON Web Token to access resources. Example: Bearer {token}",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}