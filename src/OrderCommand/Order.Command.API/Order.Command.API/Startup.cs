using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Order.Command.API.Core;
using Order.Command.API.Core.Commands;
using Order.Command.API.Projections.MongoDb;


namespace Order.Command.API
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
            services.AddCqrs();
            services.AddAutoMapper(typeof(ICommandSender));
            services.AddEventStore();
            services.AddSwaggerDocumentation();
            services.AddControllers();

            services.AddTransient<IMongoDbContext, MongoDbContext>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwaggerDocumentation();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
