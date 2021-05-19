using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopList.Models.Database;
using ShopList.Models.Database.Entities;
using ShopList.Repositories;
using ShopList.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ShopList
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
            services.AddHttpContextAccessor();

            services.AddDbContext<ShopDbContext>(o => o.UseSqlServer(
                Configuration.GetConnectionString("ShopDb")));

            services.AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2d);
                options.Lockout.MaxFailedAccessAttempts = 10;
            })
               .AddEntityFrameworkStores<ShopDbContext>()
               .AddDefaultTokenProviders();

            services.AddAuthorization()
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "https://shoplist.ro",
                        ValidAudience = "https://shoplist.ro",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            @"hfwehdfuhf0jf-23jd9-83u9830ffjn4jffkerfj4j32f9043jfjifjrefjre")),
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddScoped<ProductEntityRepository>();

            services.AddScoped<ProductEntityService>();

            services.AddScoped<CartEntityRepository>();

            services.AddScoped<CartEntityService>();

            services.AddScoped<UserRepository>();

            services.AddScoped<UserService>();

            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopList", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopList v1"));
            }

            app.UseHttpsRedirection();

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
