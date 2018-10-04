using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace TaskforLorena_work_with_DBSQLite_
{
    public interface IBDStorage
    {
        List<IDepartment> GetMainDepartments();
    }
}
    
