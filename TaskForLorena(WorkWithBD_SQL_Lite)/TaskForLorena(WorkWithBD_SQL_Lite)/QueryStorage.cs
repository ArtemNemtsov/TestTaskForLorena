using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace TaskforLorena_work_with_DBSQLite_
{
    public class QueryStorage
    {
        private string createTableSQL =
            "CREATE TABLE `TestTask` (" +
             "`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
             " `Parent_id`	INTEGER NOT NULL," +
            "`Name`	TEXT NOT NULL," +
            "`Discount" +
            "`	REAL NOT NULL," +
            "	`Relation`	INTEGER NOT NULL," +
            "	`Description`	TEXT(124)" +
            ");";

        public string GetCreateTableQuery()
        {
            return createTableSQL;
        }
    }
}
