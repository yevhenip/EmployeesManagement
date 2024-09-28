using EmployeesManagement.Core.Contracts;
using EmployeesManagement.Core.Domain;
using Mapster;

namespace EmployeesManagement.Core.UseCases.Common;

public class MapperProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Employee, EmployeeModel>()
            .Map(x => x.DepartmentName, x => x.Department.Name)
            .Map(x => x.ManagerName, x => x.Manager.Name);
    }
}