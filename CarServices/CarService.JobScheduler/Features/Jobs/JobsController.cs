
namespace CarService.JobScheduler.Features.Jobs
{
    using CarService.JobScheduler.Data;
    using CarService.JobScheduler.Features.Jobs.Models;
    using CarServices;
    using CarServices.JobScheduler.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class JobsController : ApiController
    {
        private readonly JobSchedulerDbContext dbContext;

        public JobsController(JobSchedulerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Create(JobCreateModel model)
        {
            Job job = new Job()
            {
                CreatedDate = DateTime.UtcNow,
                CreatedByEmployeeId = model.CreatedByEmployeeId,
                DeadLine = model.DeadLine,
                DepartmentId = model.DepartmentId,
                JobStatus = JobStatus.Pending,
                VehicleId = model.VehicleId,
            };

            job.PurchasedServices = model.PurchasedServices.Select(ps => new JobService()
            {
                Name = ps.Name,
                Price = ps.Price,
                ServiceId = ps.ServiceId
            }).ToList();

            this.dbContext.Add(job);

            await this.dbContext.SaveChangesAsync();

            return Created(nameof(Create), job.Id);
        }
    }
}
