namespace CarServices.Garage.Features.Vehicles
{
    using CarServices.Garage.Data;
    using CarServices.Garage.Data.Models;
    using CarServices.Garage.Features.Vehicles.Models;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static CarService.Infrastructure.InfrastructureConstants;

    public class VehicleController : ApiController
    {
        private readonly GarageDbContext dbContext;
        private readonly ICurrentEmployeeService currentEmployeeService;

        public VehicleController(GarageDbContext carServiceDbContext,
            ICurrentEmployeeService currentEmployeeService)
        {
            this.dbContext = carServiceDbContext;
            this.currentEmployeeService = currentEmployeeService;
        }

        [HttpPost]
        [Authorize(Policy = GarageAdminPolicyName)]
        public async Task<ActionResult<int>> Create(VehicleCreateRequestModel model)
        {

            Vehicle vehicle = new Vehicle()
            {
                Color = model.Color,
                Make = model.Make,
                RegistryNumber = model.RegistryNumber,
                Model = model.Model,
            };

            Garage garage = await this.dbContext.Garages.FirstOrDefaultAsync(d => d.Id == this.currentEmployeeService.CurrentGarageId);
            garage.Vehicles.Add(vehicle);

            await this.dbContext.SaveChangesAsync();

            return Created(nameof(this.Create), vehicle.Id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleListModel>>> All()
        {

            IEnumerable<Vehicle> vehicles = await this.dbContext.Garages
                .Where(g => g.Id == currentEmployeeService.CurrentGarageId)
                .Include(g => g.Vehicles)
                .SelectMany(g => g.Vehicles)
                .ToListAsync();


            IEnumerable<VehicleListModel> currentGarageVehicles = vehicles.Select(v => new VehicleListModel()
            {
                Id = v.Id,
                Model = v.Model,
                RegistryNumber = v.RegistryNumber,
            });


            return Ok(currentGarageVehicles);
        }

    }
}
