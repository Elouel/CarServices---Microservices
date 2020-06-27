

namespace CarServices.Services.Employee
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using System;

    using static CarService.Infrastructure.InfrastructureConstants;

    public class CurrentEmployeeService : ICurrentEmployeeService
    {
        public CurrentEmployeeService(
            IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }


            StringValues roleHeader = httpContextAccessor.HttpContext.Request.Headers[GarageRoleHeaderName];
            StringValues currentGarageIdHeader = httpContextAccessor.HttpContext.Request.Headers[CurrentGarageIdHeaderName];
            StringValues currentEmployeeIdHeader = httpContextAccessor.HttpContext.Request.Headers[CurrentEmployeeIdHeaderName];


            this.GarageRole = Enum.Parse<GarageRoleType>(roleHeader.ToString());
            this.CurrentEmployeeId = int.Parse(currentEmployeeIdHeader);
            this.CurrentGarageId = int.Parse(currentGarageIdHeader);
        }

        public GarageRoleType GarageRole { get; }

        public int CurrentGarageId { get; }

        public int CurrentEmployeeId { get; }

        public bool IsAdmin => this.GarageRole == GarageRoleType.Admin;

        public bool IsSalesman => this.GarageRole == GarageRoleType.Salesman;

        public bool IsOperator => this.GarageRole == GarageRoleType.Operator;
    }
}
