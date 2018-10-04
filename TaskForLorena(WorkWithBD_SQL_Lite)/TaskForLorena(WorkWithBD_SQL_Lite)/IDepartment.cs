using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskforLorena_work_with_DBSQLite_
{
    public interface IDepartment
    {
        int parent_id { get; }
        int ID { get; }
        double Discount { get; set; }
        bool DependsOnParent { get; set; }
        void AddChild(IDepartment dep); 
        List<IDepartment> ChildDepartments { get; set; }
    }
}
