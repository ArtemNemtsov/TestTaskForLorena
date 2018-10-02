﻿using System;
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

        private readonly SQLiteConnection dbConnection;          //поле для хранения экземпляра dbConnection ,уже соединеного с нашей БД

        public BDStorage (string connectionPath)
        {            
            dbConnection = CreateDBConnection(connectionPath);
            QueryStorage queryStorObj = new QueryStorage(dbConnection);   //создаем экзепляр класса QueryStorage  и передаем конструктору dbConnection
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


     //   SQLiteCommand command = new SQLiteCommand(select, dbConnection);
    }    
}
