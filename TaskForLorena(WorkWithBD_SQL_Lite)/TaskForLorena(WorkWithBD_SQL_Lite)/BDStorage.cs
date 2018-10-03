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

       public bool CreateNewTableBD(string createTableSQL)
       {
           command = new SQLiteCommand(createTableSQL, dbConnection);
           count = command.ExecuteNonQuery();
           if (count > 0)
           {
               return true;
           }
           return false;
       }

       public bool InsertValueBD(string FillTableSQL)
       {
           count = 0;
           command = new SQLiteCommand(FillTableSQL, dbConnection);
           count= command.ExecuteNonQuery();
           if (count > 0)
           {
               return true;
           }
           return false;
       }

       public bool ClearTable(string nameTable)
       {
           count = 0;
           string clearTable = "DELETE * FROM " + nameTable;
           command = new SQLiteCommand(clearTable, dbConnection);
           if (count > 0)
           {
               return true;
           }
           return false;
       }

       public bool DeletedTableFromBD(string nameTable)
       {
           count = 0;
           string deleteTable = "DROP TABLE " + nameTable;
           command = new SQLiteCommand(deleteTable, dbConnection);
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

          
    }    
}
