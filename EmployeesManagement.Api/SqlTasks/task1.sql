select e.*
from Employee e
         join Employee m on e.ManagerID = m.ID
where e.Salary > m.Salary