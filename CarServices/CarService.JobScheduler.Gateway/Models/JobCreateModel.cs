
namespace CarService.JobScheduler.Gateway.Models
{
    using System;
    using System.Collections.Generic;

    public class JobCreateModel
    {
        public int CreatedByEmployeeId { get; set; }

        public DateTime DeadLine { get; set; }

        public int DepartmentId { get; set; }

        public int VehicleId { get; set; }

        public IEnumerable<int> PurchasedServices { get; set; }
    }
}
