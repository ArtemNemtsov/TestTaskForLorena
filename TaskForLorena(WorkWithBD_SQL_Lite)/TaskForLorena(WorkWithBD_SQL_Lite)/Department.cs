using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskForLorena_WorkWithBD_SQL_Lite_
{
    public class Department : IDepartment
    {
        public double Discount { get; set; }
        public List<IDepartment> ChildDepartments { get; set; }
        public bool DependsOnParent { get; set; }

       
        public Department(double Discount, List<IDepartment> ChildDepartments, bool DependsOnParent)
        {
            this.Discount = Discount;
            this.ChildDepartments = ChildDepartments;
            this.DependsOnParent = DependsOnParent;
        }
    }
}
