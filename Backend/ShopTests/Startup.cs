using Castle.Core.Configuration;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Database;
using Shop.Model;
using Shop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTests
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup()
        {
  
        }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProductRepository<Product>, ProductRepository>();

  
            services.AddMediatR(typeof(Shop.Startup));
            
            AddDbContext(services);
        }

        public void AddDbContext(IServiceCollection services)
        {
            var locaConnection = "Data Source=DESKTOP-M5L262K\\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True";
            
            services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(locaConnection)
            .UseLazyLoadingProxies());
        }
    }
}
