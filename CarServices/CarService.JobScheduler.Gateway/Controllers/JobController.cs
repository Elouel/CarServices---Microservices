

namespace CarService.JobScheduler.Gateway.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using CarServices;
    using Microsoft.AspNetCore.Authorization;

    using static CarService.Infrastructure.InfrastructureConstants;
    using CarService.JobScheduler.Gateway.Services.Garages;
    using CarService.JobScheduler.Gateway.Models;

    public class JobController : ApiController
    {
        private readonly IGarageService garageService;

        public JobController(IGarageService garageService)
        {
            this.garageService = garageService;
        }

        [HttpPost]
        [Authorize(Policy = GarageSalesmanPolicyName)]
        public async Task<ActionResult> Create(JobCreateModel model)
        {

            var services = await this.garageService.Services(model.DepartmentId);



            return Ok();
        }
    }
}
