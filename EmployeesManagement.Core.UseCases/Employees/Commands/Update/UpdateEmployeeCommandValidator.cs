using FluentValidation;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Update;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(512);
        RuleFor(x => x.Salary).ExclusiveBetween(0, 100000);
        RuleFor(x => x.DepartmentId).GreaterThan(0);
        RuleFor(x => x.ManagerId).GreaterThan(0);
    }
}