using EmployeesManagement.Core.Exceptions;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Delete;

public class DeleteManagerException : DomainException
{
    public override string Code => "DeleteManager";

    public DeleteManagerException(int id) : base($"Cannot delete manager with id {id}")
    {
        Details = new Dictionary<string, object?>
        {
            ["id"] = id
        };
    }
}