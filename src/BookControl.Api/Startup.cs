using BookControl.Business.Interfaces;
using BookControl.Business.Services;
using BookControl.Data.Context;
using BookControl.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookControl.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookControlDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpClient();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddScoped<BookControlDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IStudentService, StudentService>();  
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
