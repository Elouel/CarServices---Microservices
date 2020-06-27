
namespace CarServices.Garage.Features.Employees
{

    using CarServices.Garage.Features.Employees.Models;
    using CarServices.Services.Employee;
    using System.Threading.Tasks;

    public interface IEmployeeService
    {
        Task<int> CreateEmployee(EmployeeCreateRequestModel model);
        Task<EmployeeResponseModel> GetEmployeeByUser(string userId);

        Task<GarageRoleType> GetEmployeeRoleByUserId(string userId);
    }
}
