using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskforLorena_work_with_DBSQLite_
{
    public class Department : IDepartment
    {
        public int ID { get; }
        public int  parent_id { get; }
        public double Discount { get; set; } 
        public bool DependsOnParent { get; set; }
        public List<IDepartment> ChildDepartments { get; set; }

        public Department(int ID, int parent_id, double Discount, bool DependsOnParent)
        {
            this.ID = ID;
            this.parent_id = parent_id;
            this.Discount = Discount;
            this.DependsOnParent = DependsOnParent;
        }

        public void AddChild(IDepartment dep)
        {
            ChildDepartments.Add(dep);
        }
    }
}
