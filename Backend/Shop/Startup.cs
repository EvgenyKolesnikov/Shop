using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Model;
using Shop.Repository;
using System.Reflection;

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
            services.AddMvc().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddLogging();
            services.AddSwaggerGen(options => {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddMediatR(typeof(Startup));

            AddDbContext(services);


            services.AddTransient<IProductRepository<Product>, ProductRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            //app.UseCors();
            // app.UseAuthorization();
            app.UseCors(builder =>
            {

                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
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
            var locaConnection = Configuration.GetConnectionString("DefaultConnection");
          
            services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(serverconnection)
           // options.UseSqlite($"Filename=D:\\ShopDb.db")
            .UseLazyLoadingProxies());
        }
    }
}
