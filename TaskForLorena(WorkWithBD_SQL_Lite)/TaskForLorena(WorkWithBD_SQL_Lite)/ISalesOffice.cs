using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskforLorena_work_with_DBSQLite_
{
    public interface ISalesOffice
    {
        string Name { set; get; }
        float Discount { set; get; }
        bool Relation { set;  get; }        
    }
}
