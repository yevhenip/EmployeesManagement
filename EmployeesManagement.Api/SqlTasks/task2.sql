select e.Name as EmployeeName, e.Salary, d.Name as DepartmentName
from Employee e
         join Department d on e.DepartmentID = d.ID
where e.Salary = (
    select max(e2.Salary)
    from Employee e2
    where e2.DepartmentID = e.DepartmentID
);