using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace TaskforLorena_work_with_DBSQLite_
{
    public interface IBDStorage
    {
        SQLiteConnection CreateDBConnection(string connectionPath);
        int CreateCellsOffice(string Name, float discount, bool dependence, string description, int IDparent);
        bool ClearCellsOffice(string nameTable);
        bool IsExistTableStatus(string NameTable);
    }
}
