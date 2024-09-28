using System.Reflection;

namespace EmployeesManagement.Core.UseCases;

public static class AssemblyMarker
{
    public static string Name => typeof(AssemblyMarker).Assembly.GetName().Name!;
    public static Assembly Assembly => typeof(AssemblyMarker).Assembly;
}