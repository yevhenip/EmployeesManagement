select d.Name
from Employee e
         join Department d on e.DepartmentID = d.ID
group by d.Name
having count(d.ID) > 50