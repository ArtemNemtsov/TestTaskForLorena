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
        private readonly SQLiteConnection dbConnection;


        public BDStorage (string connectionPath)
        {
            dbConnection = CreateDBConnection(connectionPath); 
        }

       public SQLiteConnection CreateDBConnection(string connectionPath)          // метод для соединения к БД
       {
            if (!(File.Exists(connectionPath)))                             // если файл не найден сообщаем об ошибке 
            {
                MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;  // there is no db-file
            }
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=" + connectionPath);
            dbConnection.Open();
            return dbConnection;
        }
    }    
}
