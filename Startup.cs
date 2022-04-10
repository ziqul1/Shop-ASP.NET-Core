using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projektos.DAL;
using Microsoft.AspNetCore.Http;
using Projektos.Utils;

namespace Projektos
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
			services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication", config =>
			{
				config.Cookie.HttpOnly = true;
				config.Cookie.SecurePolicy = CookieSecurePolicy.None;
				config.Cookie.Name = "UserLoginCookie";
				config.LoginPath = "/Login/UserLogin";
				config.Cookie.SameSite = SameSiteMode.Strict;
			});

			
			services.AddRazorPages().AddMvcOptions(options =>
			{
				options.Filters.Add(new CustomPageFilter(Configuration));
			});
			

			services.AddRazorPages(options =>
			{
				options.Conventions.AuthorizeFolder("/admin");
			});


			services.AddRazorPages().AddRazorRuntimeCompilation();
			services.Add(new ServiceDescriptor(typeof(IProductDB), new ProductSqlDB(Configuration)));
			services.Add(new ServiceDescriptor(typeof(IUserDB), new UserSqlDB(Configuration)));

			
			services.AddMemoryCache();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
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
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseCookiePolicy();
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
