select top 1 d.Name as DepartmentName, sum(e.Salary) as ts
from Department d
         join Employee e on d.ID = e.DepartmentID
group by d.Name
order by ts desc;