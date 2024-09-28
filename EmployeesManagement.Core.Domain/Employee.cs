namespace EmployeesManagement.Core.Domain;

public class Employee
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required double Salary { get; set; }
    public required int ManagerId { get; set; }
    public required int DepartmentId { get; set; }
    public required Employee Manager { get; set; }
    public required Department Department { get; set; }
}