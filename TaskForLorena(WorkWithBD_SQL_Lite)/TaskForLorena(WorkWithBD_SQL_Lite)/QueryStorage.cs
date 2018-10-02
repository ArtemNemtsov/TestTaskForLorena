using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskForLorena_WorkWithBD_SQL_Lite_
{
    public class QueryStorage
    {
        public string createOurTable   =
            "CREATE TABLE `TestTask` (" +
             "`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
             " `Parent_id`	INTEGER NOT NULL," +
            "`Name`	TEXT NOT NULL," +
            "`Discount" +
            "`	REAL NOT NULL," +
            "	`Relation`	INTEGER NOT NULL," +
            "	`Description`	TEXT(124)" +
            ");" ;

        public string FillOurTabled  =
               "insert into TestTask" +
                "values" +
                "(1,0, \"Миасс\", 4,  0,  \"фабрика\")," +
                "(2,1, \"Амелия\",5,   1,    \"\")," +
                "(3,2, \"Teст1\",      2,   1,	\"\" )," +
                "(4,1, \"Тест2\",      0,   1, 	\"\" )," +
                "(5,0, \"Курган\",   11,  0,    \"badEconomy\");";
    }
}
