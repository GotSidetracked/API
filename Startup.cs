using KTU_API.Autherization;
using KTU_API.Data;
using KTU_API.Data.Dtos.Users;
using KTU_API.Data.Entities;
using KTU_API.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTU_API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var connection = "";
            services.AddDbContext<DBContextModel>(options => options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DBContextModel>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    options.TokenValidationParameters.ValidAudience = _configuration["JWT:ValidAudience"];
                    options.TokenValidationParameters.ValidIssuer = _configuration["JWT:ValidIssuer"];
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy(Constants.SameUsers, policy => policy.Requirements.Add(new SameUserRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, SameUserAuth>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddTransient<IPathRepository, PathRepository>();
            services.AddTransient<ICommentRespository, CommentRepository>();
            services.AddTransient<IGearRepository, GearRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<DatabaseSeeder, DatabaseSeeder>();
            //----
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
