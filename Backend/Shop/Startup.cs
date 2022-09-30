using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Model;
using Shop.Repository;

namespace Shop
{
    public class Startup
    {
        public IConfiguration Configuration { get;}
        public IWebHostEnvironment Environment { get;}


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddMediatR(typeof(Startup));

            AddDbContext(services);


            services.AddTransient<IProductRepository<Product>, ProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
           // app.UseAuthorization();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
