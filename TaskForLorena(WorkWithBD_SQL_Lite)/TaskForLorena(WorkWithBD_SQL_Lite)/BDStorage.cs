using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace Lorena
{
    public class DBStorage
    {
        private SQLiteConnection dbConnection = null;

        public DBStorage(string connectionString)
        {
            if (!(File.Exists(connectionString)))    // если файл не найден сообщаем об ошибке 
            {
                MessageBox.Show("File not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
            this.dbConnection = new SQLiteConnection("Data Source=" + connectionString);
            this.dbConnection.Open();
        }

        public List<IDepartment> GetMainDepartments()
        {
            // Go to DB and getting main departments
            var roodDep = new Department(dbConnection, 0); // немного читерства
            return roodDep.GetChildDepartments();
        }
        

        public bool DBTableExist(string tableName)
        {
            //Go to BD and Check IsTable there
            int count = 0;
            string SQL = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = \"" + tableName + "\"";
            SQLiteCommand command = new SQLiteCommand(SQL, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                count = int.Parse(reader["Count(*)"].ToString());
            }
            return count == 1;
        }

        private void CreateDepartmentsTable()
        {

            string query  =
                "CREATE TABLE `TestTask` (" +
                 "`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                 " `Parent_id`	INTEGER NOT NULL," +
                "`Name`	TEXT NOT NULL," +
                "`Discount" +
                "`	REAL NOT NULL," +
                "	`Relation`	INTEGER NOT NULL," +
                "	`Description`	TEXT(124)" +
                ");";
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            command.ExecuteNonQuery();
        }

        private void CreateResultsTable()
        {
            string query = "CREATE TABLE `Results` ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `department_id` INTEGER NOT NULL, `price` NUMERIC NOT NULL, `discount` NUMERIC NOT NULL DEFAULT 0, `parent_discount` NUMERIC NOT NULL DEFAULT 0, `discounted_price` NUMERIC NOT NULL );";
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            command.ExecuteNonQuery();
        }

        public IDepartment CreateDepartment(string name, float discount, bool dependency, string description, IDepartment parent = null)
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            // Вставка в таблицу
            int parentId = parent == null ? 0 : parent.Id;
            string insertQuery = "INSERT into TestTask (parent_id, Name, Discount, Relation, Description)";
            insertQuery += String.Format(" VALUES ({0}, \"{1}\", {2}, {3}, \"{4}\")", 
                parentId, name, discount.ToString(culture), dependency ? 1 : 0, description);
            SQLiteCommand command = new SQLiteCommand(insertQuery, dbConnection);
            command.ExecuteNonQuery(); 

            // получение добавленного id и формирования экземпляра департамента
            // посмотреть  last_insert_rowid () 
            string query = "select max(id) from TestTask;";
            command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
                int id = Int32.Parse((reader[0]).ToString());
                return new Department(dbConnection, id, parent);
            }
            return null;
        }

        public bool TableIsEmpty(string tableName)
        {
            string query = String.Format("SELECT COUNT(*) FROM {0};", tableName);
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int cnt = Int32.Parse(reader[0].ToString());
                return cnt == 0;
            }
            return false;
        }

        public void CreateRequiredTableIsNotExist()
        {
            string tableName = "TestTask";
            if (!DBTableExist(tableName))                            // если таблицы нет, создаем ее
            {
                CreateDepartmentsTable();
            }

            tableName = "Results";
            if (!DBTableExist(tableName))                            // если таблицы нет, создаем ее
            {
                CreateResultsTable();
            }
        }

        public void LogResult(IDepartment dep, double price, double parentDiscount, double discountedPrice)
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            String query = 
                String.Format("INSERT INTO Results(department_id, price, discount, parent_discount, discounted_price)" +
                " VALUES ({0}, {1}, {2}, {3}, {4});",
                dep.Id, price.ToString(culture), 
                dep.Discount.ToString(culture), parentDiscount.ToString(culture), discountedPrice.ToString(culture));
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            command.ExecuteNonQuery();
        }
    }
}


    

       



