using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskForLorena_WorkWithBD_SQL_Lite_
{
    public interface IDepartment
    {
        double Discount { get; set; }
        List<IDepartment> ChildDepartments { get; set; }
        bool DependsOnParent { get; set; }
    }
}
