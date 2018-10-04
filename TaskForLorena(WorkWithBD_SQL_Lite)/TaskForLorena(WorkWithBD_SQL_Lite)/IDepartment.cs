using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskforLorena_work_with_DBSQLite_
{
    public interface IDepartment
    {
        List<IDepartment> GetChildDepartments();
        IDepartment Parent { get; }      
        int Id { get; }
        String Name { get; }         // Имя департамента
        Double Discount { get; }
        Boolean IsDependent { get; }
        String Description { get; }
    }
}
