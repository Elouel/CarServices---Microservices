

namespace CarService.JobScheduler.Gateway.Services.Garages
{
    using CarService.JobScheduler.Gateway.Models;
    using CarServices.JobScheduler.Gateway.Models;
    using Refit;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGarageService
    {
        [Get("/Employee/{id}")]
        Task<EmployeeResponseModel> Details(string id);

        [Get("/Department/{id}/Services")]
        Task<IEnumerable<ServiceOutputModel>> Services(int id);
    }
}
