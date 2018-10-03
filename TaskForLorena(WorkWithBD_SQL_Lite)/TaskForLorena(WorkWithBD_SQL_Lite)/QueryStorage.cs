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

        private string FillTableSQL  =
              "insert into TestTask" +
               "values" +
               "(1,0, \"Миасс\", 4,  0,  \"фабрика\")," +
               "(2,1, \"Амелия\",5,   1,    \"\")," +
               "(3,2, \"Teст1\",      2,   1,	\"\" )," +
               "(4,1, \"Тест2\",      0,   1, 	\"\" )," +
               "(5,0, \"Курган\",   11,  0,    \"badEconomy\");" ;

        public string GetCreateTableQuery()
        {
            return createTableSQL;
        }

        public string GetFillTableQuery()
        {
            return FillTableSQL;
        }
    }
}
