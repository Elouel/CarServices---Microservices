

namespace CarService.JobScheduler.Features.Jobs.Models
{
    public class JobServiceCreateModel
    {
        public int ServiceId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }
    }
}
