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

        public BDStorage(string connectionPath)
        {
            dbConnection = CreateDBConnection(connectionPath); //создаем экзепляр класса QueryStorage  и передаем конструктору dbConnection             
        }

        public List<string> GetShops()
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

        public bool IsExistTableStatus(string NameTable)
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

        public void LoadTabletoBD(string SQLCreateTableScript)
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

        public List<IDepartment> GetSalesOffice()
        {
            int id;
            int childID;
            float discount;
            int parent_id;
            float childDiscountl;
            bool relatioSalesOfficen = false;
            bool childRelationSales = false;
            string querySQL = "select ID,Parent_id, Discount,Relation FROM TestTask";

            List<IDepartment> depsWithoutParent = new List<IDepartment>();    //будем хранить все департаменты для которых нет родителей
            List<IDepartment> deps = new List<IDepartment>();                //здесь будем хранить все департаменты
            List<int> depsParentIds = new List<int>();                   // хранятся индетификаторы родителей
            command = new SQLiteCommand(querySQL, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int N = 10; // создаем 10 департментов

            while (reader.Read())
            {
                relatioSalesOfficen = false;
                parent_id = Int32.Parse(reader["ID"].ToString());
                id = Int32.Parse(reader["ID"].ToString());
                discount = float.Parse(reader["Discount"].ToString());
                if (int.Parse(reader["Relation"].ToString()) == 1)
                {
                    relatioSalesOfficen = true;
                }
                var dep = new Department(id, parent_id, discount, relatioSalesOfficen);    // создаем департамент по ID
                if (parent_id == 0)                                                //если нет родителя
                {
                    depsWithoutParent.Add(dep);               //добавляем в лист без родителей;
                }
                deps.Add(dep);
                depsParentIds.Add(parent_id);
            }

            for (int i = 0; i < deps.Count; ++i)     // создаем родственные связи
            {
                var dep = deps[i];                 //берем департамент
                var parentID = depsParentIds[i];   //берем ID его родителя

                foreach(var parDep in deps)        //ищем департамент с таким id в списке deps
                {
                    if (parDep.ID == parentID)
                    {
                        parDep.AddChild(dep);             // родитель найден, добавляем ему ребенка
                            break;                          
                    }
                }

            }
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
