

namespace CarServices.Garage.Infrastructure.Middlewares
{
    using CarService.JobScheduler.Gateway.Services.Garages;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using static CarService.Infrastructure.InfrastructureConstants;

    public class CurrentEmployeeMiddleware : IMiddleware
    {
        private readonly IGarageService garageService;

        public CurrentEmployeeMiddleware(IGarageService employeeService)
        {
            this.garageService = employeeService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentEmployeeGarage = this.garageService.Details(userId).GetAwaiter().GetResult();

            context.Request.Headers.Add(GarageRoleHeaderName, currentEmployeeGarage.RoleDisplayName);
            context.Request.Headers.Add(CurrentGarageIdHeaderName, currentEmployeeGarage.GarageId.ToString());
            context.Request.Headers.Add(CurrentEmployeeIdHeaderName, currentEmployeeGarage.EmployeeId.ToString());

            await next.Invoke(context);
        }
    }
}
