namespace CarServices.Garage.Features.Garages
{
    using CarService.Services.Identity;
    using CarServices.Garage.Features.Garages.Models;
    using CarServices.Garage.Features.Vehicles.Models;
    using CarServices.Services.Employee;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static CarService.Infrastructure.InfrastructureConstants;
    using static CarServices.Infrastructure.WebConstants;

    public class GarageController : ApiController
    {
        private readonly IGarageService garageService;
        private readonly ICurrentEmployeeService currentEmployeeService;
        private readonly ICurrentUserService currentUserService;

        public GarageController(IGarageService garageService,
             ICurrentEmployeeService currentEmployeeService,
            ICurrentUserService currentUserService)
        {
            this.garageService = garageService;
            this.currentEmployeeService = currentEmployeeService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<GarageResponseModel>> Get(int id)
        {
            var garage = await this.garageService.GetGarage(id);

            if (this.currentEmployeeService.IsAdmin)
            {
                return new GarageResponseModel()
                {
                    Id = garage.Id,
                    GarageName = garage.Name,
                    Employees = garage.Employees.Select(e =>
                    {
                        return new EmployeeListResponseModel()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Role = e.Role.ToString(),
                        };
                    }).ToList(),
                    Departments = garage.Departments.Select(d =>
                    {
                        return new DepartmentListResponseModel()
                        {
                            Id = d.Id,
                            Name = d.Name,
                        };
                    }).ToList(),
                    Vehicles = garage.Vehicles.Select(v =>
                    {
                        return new VehicleListModel()
                        {
                            Id = v.Id,
                            Model = v.Model,
                            RegistryNumber = v.RegistryNumber
                        };
                    }).ToList()
                };
            }
            else
            {
                return new GarageResponseModel()
                {
                    Id = garage.Id,
                    GarageName = garage.Name,
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<GarageResponseModel>> Create(GarageCreateRequestModel model)
        {
            return await this.garageService.CreateGarage(model, this.currentUserService.UserId);
        }

        [HttpGet]
        [Route("Departments/{id}")]
        [Authorize(Policy = GarageAdminPolicyName)]
        public async Task<IEnumerable<DepartmentListResponseModel>> Departments(int id)
        {
            var result = await this.garageService.GetGarageDepartments(id);

            return result.Select(d =>
            {
                return new DepartmentListResponseModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                };
            });
        }
    }
}
