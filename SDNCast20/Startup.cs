using ConsoleApplication.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IJwtProvider, JwtProvider>()
                .AddSingleton<IConsumerValidator, ConsumerValidator>()
                .AddAuthentication();

            services.AddMvc();

        }
        public void Configure(IApplicationBuilder app, IJwtProvider jwtProvder)
        {
            app.UseJwtBearerAuthentication(jwtProvder.Options);
            app.UseMvc();
        }
    }
}