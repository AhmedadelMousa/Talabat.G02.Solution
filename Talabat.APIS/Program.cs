
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options=>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );
            //builder.Services.AddScoped<IGenericRepository<Product>, IGenericRepository<Product>>();
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));





            var app = builder.Build();

            using var scope=app.Services.CreateScope();
            var Services=scope.ServiceProvider;
            var _dbcontext=Services.GetRequiredService<StoreContext>();
            var LoggerFactory=Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
            }
            catch (Exception ex) 
            {
                var logger= LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occurred during migration");  

            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
