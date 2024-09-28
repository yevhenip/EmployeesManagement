namespace EmployeesManagement.Core.Exceptions;

public class ValidationException : DomainException
{
    public override string Code => "ValidationError";

    public ValidationException(Dictionary<string, string[]> errors)
        : base("Validation error occured")
    {
        Details = new Dictionary<string, object?>();

        foreach (var (key, value) in errors)
        {
            Details.Add(key, value);
        }
    }
}