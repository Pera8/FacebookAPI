using FacebookApiTest.Repository;
using FacebookApiTest.Repository.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Repository.Repository;
using Service;
using Service.Communication;
using Shared.Helper;
using System.Text;
using Microsoft.OpenApi.Models;
using System;

namespace FacebookApiTest
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));
            services.AddSingleton<IUserTwoFactorTokenProvider<User>, DataProtectorTokenProvider<User>>();
            services.AddIdentity<User, Role>(opt =>
            {
                opt.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<User>)));
            }).AddEntityFrameworkStores<AppDbContext>();


            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = appSettings.BaseUrl,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidAudience = appSettings.BaseUrl,
                    };
                });

            // reset password token expiration
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.Name = "Default";
                opt.TokenLifespan = System.TimeSpan.FromHours(24);
            });

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<AuthService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<CommentsService>();
            services.AddTransient<PostesService>();
            services.AddTransient<AuthTokenService>();
            services.AddTransient<UsersService>();

            services.AddControllers();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));



            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CanUpdateUsers", policyBuilder =>
            //    policyBuilder.RequireAssertion(context => context.User.HasClaim(claim => claim.Value == "CanUpdateUsers" || claim.Value == "CanUpdateOnlySelf")));

            //});


            services.AddSwaggerGenNewtonsoftSupport();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddMvc();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core API", Description = "Swagger Core API" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                         Reference = new OpenApiReference {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                    }
                });

            });

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));




            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}