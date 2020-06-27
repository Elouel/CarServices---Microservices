﻿namespace CarService.JobScheduler.Gateway.Models
{
    using System.Collections.Generic;

    public class ServiceQuery
    {
        public int DepartmentId { get; set; }

        public IEnumerable<int> Services { get; set; }
    }
}
