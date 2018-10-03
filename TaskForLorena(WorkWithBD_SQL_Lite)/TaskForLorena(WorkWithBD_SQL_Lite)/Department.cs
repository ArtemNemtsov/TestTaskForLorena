using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskforLorena_work_with_DBSQLite_
{
    public class Department : IDepartment
    {
        public int ID { get; }
        public double Discount { get; set; }
        public List<IDepartment> ChildDepartments { get; set; }
        public bool DependsOnParent { get; set; }
        
        public Department(int ID, double Discount, List<IDepartment> ChildDepartments, bool DependsOnParent)
        {
            this.Discount = Discount;
            this.ChildDepartments = ChildDepartments;
            this.DependsOnParent = DependsOnParent;
        }
    }
}
