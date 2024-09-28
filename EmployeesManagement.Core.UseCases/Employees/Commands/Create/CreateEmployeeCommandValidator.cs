using FluentValidation;

namespace EmployeesManagement.Core.UseCases.Employees.Commands.Create;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(512);
        RuleFor(x => x.Salary).ExclusiveBetween(0, 100000);
        RuleFor(x => x.DepartmentId).GreaterThan(0);
        RuleFor(x => x.ManagerId).GreaterThan(0);
    }
}