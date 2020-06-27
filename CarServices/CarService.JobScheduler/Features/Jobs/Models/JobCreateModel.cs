using System;
using System.Collections.Generic;

namespace CarService.JobScheduler.Features.Jobs.Models
{
    public class JobCreateModel
    {
        public int CreatedByEmployeeId { get; set; }

        public DateTime DeadLine { get; set; }

        public int DepartmentId { get; set; }

        public int VehicleId { get; set; }

        public IEnumerable<JobServiceCreateModel> PurchasedServices { get; set; }
    }
}
