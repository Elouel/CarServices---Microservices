

namespace CarService.JobScheduler.Gateway
{
    using CarRentalSystem.Infrastructure;
    using CarService.Infrastructure.Middlewares;
    using CarService.JobScheduler.Gateway.Services;
    using CarService.JobScheduler.Gateway.Services.Garages;
    using CarService.Services.Identity;
    using CarServices.Garage.Infrastructure;
    using CarServices.Garage.Infrastructure.Middlewares;
    using CarServices.Infrastructure;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Refit;

    using static CarService.Infrastructure.InfrastructureConstants;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
              .GetSection(nameof(ServiceEndpoints))
              .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddTokenAuthentication(this.Configuration)
                 .AddAuthorization(config =>
                 {
                     config.AddPolicy(GarageAdminPolicyName, policybuilder => policybuilder.Requirements.Add(new AdminRequired()));
                     config.AddPolicy(GarageSalesmanPolicyName, policybuilder => policybuilder.Requirements.Add(new SalesmanRequired()));
                 })
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<ICurrentEmployeeService, CurrentEmployeeService>()
                .AddTransient<JwtHeaderAuthenticationMiddleware>()
                .AddTransient<CurrentEmployeeMiddleware>()
                .AddScoped<IAuthorizationHandler, AdminRequiredHandler>()
                .AddScoped<IAuthorizationHandler, SalesmanRequiredHandler>()
                .AddControllers();

            services
              .AddRefitClient<IGarageService>()
              .WithConfiguration(serviceEndpoints.Garages);

            //services
            //    .AddRefitClient<ICarAdViewService>()
            //    .WithConfiguration(serviceEndpoints.Statistics);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseJwtHeaderAuthentication();
            app.UseMiddleware<CurrentEmployeeMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
