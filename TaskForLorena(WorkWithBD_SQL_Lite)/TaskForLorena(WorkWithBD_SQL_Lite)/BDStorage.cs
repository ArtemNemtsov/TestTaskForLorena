using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace TaskforLorena_work_with_DBSQLite_
{
    class BDStorage : IBDStorage
    {
        int count;
        SQLiteCommand command;
        private readonly SQLiteConnection dbConnection;        //поле для хранения экземпляра dbConnection ,уже соединеного с нашей БД

        public BDStorage (string connectionPath)
        {          
            dbConnection = CreateDBConnection(connectionPath); //создаем экзепляр класса QueryStorage  и передаем конструктору dbConnection             
        }

        public List<string> GetShops ()
        {
            List<string> shopList = new List<string>();
            string SQL = "SELECT NAME FROM TestTask;";
            command = new SQLiteCommand(SQL, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                shopList.Add((reader["name"]).ToString());
            }
            return shopList;
        }

       public SQLiteConnection CreateDBConnection(string connectionPath)       // метод для соединения к БД
       {
            if (!(File.Exists(connectionPath)))                               // если файл не найден сообщаем об ошибке 
            {
                MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;  // there is no db-file
            }
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=" + connectionPath);
            dbConnection.Open();
            return dbConnection;
        }

        public bool IsExistTableStatus (string NameTable)
        {
            int count = 0;
           string SQL = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = \"" + NameTable + "\"";
            command = new SQLiteCommand(SQL, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                count = int.Parse(reader["Count(*)"].ToString());
            }
            if (count == 1)
                return true;
            return false;
        }

        public void LoadTabletoBD (string SQLCreateTableScript)
        {
            command = new SQLiteCommand(SQLCreateTableScript, dbConnection);
            command.ExecuteNonQuery();
        }

        public int CreateCellsOffice(string Name, float discount, bool Relation, string description, int IdParent)
        {
            int currentID = 0;
            int relations = 0;
            string comma = ", ";
            string qts = "\"";
            if (Relation) relations = 1;
            if (description == "") description = " emptyDescription";
            string SQLGetMaxID = "select max(id) from TestTask;";                    
            string SQLQuery = "insert into TestTask (parent_id, Name, Discount, Relation, Description)";
            SQLQuery += " values (" + IdParent + comma + qts+Name+qts + comma + discount;
            SQLQuery +=  comma + relations + comma + qts+description+qts + ")";
            command = new SQLiteCommand(SQLQuery, dbConnection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand(SQLGetMaxID, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
           while (reader.Read())
                currentID = Int32.Parse((reader["max(id)"]).ToString());
            return currentID;
       }

       public bool ClearCellsOffice(string nameTable)
       {
           count = 0;
           string clearTable = "DELETE FROM " + nameTable;
           command = new SQLiteCommand(clearTable, dbConnection);
           if (count > 0)
           {
               return true;
           }
           return false;
       }

       public List<SalesOffice> GetFieldFromBD(string querySelectSQL)
       {
           int ID;
           int Parent_id;
           string Name;
           float Discount;
           bool Relation;
           string Description;
           List<SalesOffice> ListFieldObj = new List<SalesOffice>();
           SalesOffice saleOfficeObj;
           command = new SQLiteCommand(querySelectSQL, dbConnection);
           SQLiteDataReader reader = command.ExecuteReader();
           while (reader.Read())
           {
               ID = Int32.Parse(reader["ID"].ToString());
               Parent_id = Int32.Parse(reader["Parent_id"].ToString());
               Name = reader["Name"].ToString();
               Discount = float.Parse(reader["Discount"].ToString());
               Relation = bool.Parse(reader["Relation"].ToString());
               Description = reader["Description"].ToString();
               saleOfficeObj = new SalesOffice(ID, Parent_id, Name, Discount, Relation, Description);
               ListFieldObj.Add(saleOfficeObj);
           }
           return ListFieldObj;
       }

        public string createTableSQL =
          "CREATE TABLE `TestTask` (" +
           "`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
           " `Parent_id`	INTEGER NOT NULL," +
          "`Name`	TEXT NOT NULL," +
          "`Discount" +
          "`	REAL NOT NULL," +
          "	`Relation`	INTEGER NOT NULL," +
          "	`Description`	TEXT(124)" +
          ");";
    }    
}
