using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RadioBatton
{
	public class Startup
	{
		private char[] securityKey;
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public Startup(HostingEnvironment env)
		{
			Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
		}

		public IConfiguration Configuration { get; }
		//public static string ConnectionString { get; private set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			string securityKey = "This_is_my_key_for_diploma_project";
			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
			
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						//what to validate
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateIssuerSigningKey = true,
						//setup validate data
						ValidIssuer = "smesk.in",
						ValidAudience = "readers",
						IssuerSigningKey = symmetricSecurityKey
					};
				});
			services.AddCors(c =>
			{
				c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			services.AddSingleton<RadioBattonDbContext>();
			services.AddSingleton<SongRepository>();
			services.AddSingleton<UserRepository>();
			services.AddSingleton<GenreRepository>();
			services.AddSingleton<LikeRepository>();
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "RadioBatton",
					Description = "Diploma work",
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseCors(options => options.AllowAnyOrigin());
			app.UseStaticFiles();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

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
