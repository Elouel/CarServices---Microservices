namespace CarServices.JobScheduler.Data.Models
{
    using CarService.JobScheduler.Features.Jobs;
    using System;
    using System.Collections.Generic;

    public class Job
    {
        public int Id { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public int? AssignedEmployeeId { get; set; }

        public JobStatus JobStatus { get; set; }

        public DateTime DeadLine { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? StratedDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public int DepartmentId { get; set; }

        public int VehicleId { get; set; }

        public IList<JobService> PurchasedServices { get; set; }
    }
}
