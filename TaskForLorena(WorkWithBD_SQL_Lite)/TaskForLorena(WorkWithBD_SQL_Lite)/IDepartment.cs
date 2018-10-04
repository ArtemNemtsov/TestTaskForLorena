using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lorena
{
    public interface IDepartment
    {
        List<IDepartment> GetChildDepartments();
        IDepartment Parent { get; }      
        int Id { get; }
        String Name { get; }
        Double Discount { get; }
        Boolean IsDependent { get; }
        String Description { get; }
    }
}
