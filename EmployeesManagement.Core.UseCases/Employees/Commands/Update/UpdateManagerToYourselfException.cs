using EmployeesManagement.Core.Exceptions;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Update;

public class UpdateManagerToYourselfException : DomainException
{
    public override string Code => "UpdateManagerToYourself";

    public UpdateManagerToYourselfException(int id) : base($"Cannot set manager id to yourself")
    {
        Details = new Dictionary<string, object?>
        {
            ["id"] = id
        };
    }
}