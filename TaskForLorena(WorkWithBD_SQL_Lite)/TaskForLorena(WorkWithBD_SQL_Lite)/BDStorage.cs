using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace TaskforLorena_work_with_DBSQLite_
{
    public class BDStorage : IBDStorage
    {
        private SQLiteCommand command;
        readonly private SQLiteConnection dbConnection;

        public BDStorage (string connectionString)
        {
            if (!(File.Exists(connectionString)))    // если файл не найден сообщаем об ошибке 
            {
                MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=" + connectionPath);
            dbConnection.Open();
        }

        public List<IDepartment> GetMainDepartments()
        {
            // Go to DB and getting main departments
            int id;
            string querySQL = "select ID,Parent_id, Discount,Relation FROM TestTask WHERE parent_id = 0";
            command = new SQLiteCommand(SQL, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            var deps = new List<IDepartment>();
            while (reader.Read())
            {
                relatioSalesOfficen = false;
                id = Int32.Parse(reader["id"].ToString());
                deps.Add(new DBDepartment(dbConnection, id));
            }
            return deps;
        }
        public bool DBTableExist(string NameTable)
        {
            //Go to BD and Check IsTable there
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

         public CreateTabletoBD (string ScriptAddTable)
        {
            command = new SQLiteCommand(ScriptAddTable, dbConnection);
            command.ExecuteNonQuery();
        }

        public int CreateCellsOffice(string Name, float discount, bool Relation, string description, int IdParent)
        {
            // Create in BD table  string 
            int currentID = 0;
            int relations = 0;
            string comma = ", ";
            string qts = "\"";
            if (Relation) relations = 1;
            if (description == "") description = " emptyDescription";
            string SQLGetMaxID = "select max(id) from TestTask;";
            string SQLQuery = "insert into TestTask (parent_id, Name, Discount, Relation, Description)";
            SQLQuery += " values (" + IdParent + comma + qts + Name + qts + comma + discount;
            SQLQuery += comma + relations + comma + qts + description + qts + ")";
            command = new SQLiteCommand(SQLQuery, dbConnection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand(SQLGetMaxID, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                currentID = Int32.Parse((reader["max(id)"]).ToString());
            return currentID;
        }
    }
}


    

       



