using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskforLorena_work_with_DBSQLite_
{
    public class SalesOffice : ISalesOffice
    {
        public int ID { get; }
        public int Parent_id { set; get; }
        public string Name { set; get; }
        public float Discount { set; get; }
        public bool Relation { set; get; }
        public string Description                          // ставим ограничение на длину  строки не более 124
        {
            get { return Description; }
            set
            {   
                if (value.Length > 124)
                {
                    Description = value.Substring(0, 124);
                }                else Description = value;
            }
        }

        public SalesOffice(int ID, int Parent_id, string Name, float Discount, bool Relation, string Description)  //конструктор
        {
            this.ID = ID;
            this.Parent_id = Parent_id;
            this.Name = Name;
            this.Discount = Discount;
            this.Relation = Relation;
            this.Description = Description;
        }

    }
}