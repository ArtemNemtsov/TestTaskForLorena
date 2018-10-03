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
         bool CreateNewTableBD( string createTableSQL);
         bool InsertValueBD(string FillTableSQL);
         bool DeletedTableFromBD(string nameTable);
         List<SalesOffice> GetFieldFromBD(string querySelectSQL);
    }
}
