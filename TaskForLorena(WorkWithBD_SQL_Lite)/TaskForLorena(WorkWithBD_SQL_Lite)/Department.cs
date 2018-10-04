using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskforLorena_work_with_DBSQLite_
{
    public class Department : IDepartment
    {
        private SQLiteCommand command;
        private readonly SQLiteConnection dbconnection;
        public IDepartment Parent { get; }
        public int Id { get; }

        public DBDepartment(SQLiteConnection connection, int id, IDepartment parent = null)
        {
            Id = id;
            this.connection = connection;
            Parent = parent;
        }

        public List<IDepartment> GetChildDepartments()
        {
            var children = new List<IDepartment>();                 //  идем в бд и по индексу this.id нахоидм id всех детей          
            int childId;                                            // всех детей складываем в список children 
            string querySQL = "select id from TestTable where parent_id = " + Id;
            command = new SQLiteCommand(SQL, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                childId = int.Parse(reader["id"]);
                IDepartment childDepartment = new DBDepartment(dbconnection, childId, this);   // this- говорим кто родитель
                children.Add(childDepartment);
            }
            return children;
        }

        public String Name
        {
            get   // используя id и connecton получаем из бд имя 
            {
                string name;
                string querySQL = "select Name FROM TestTask WHERE id = " + Id;
                command = new SQLiteCommand(SQL, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    name = reader["Name"].ToString();
                }
                return name;
            }
        }

        public Double Discount
        {
            get
            {
                double discount;
                string querySQL = "select Discount FROM TestTask WHERE id = " + Id;
                command = new SQLiteCommand(SQL, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    discount = double.Parse(reader["Discount"]);
                }
                return discount;
            }   
        }

        public Boolean IsDependent
        {
            get
            {
                bool isDependent = true;
                string querySQL = "select Relation FROM TestTask WHERE id = " + Id;
                command = new SQLiteCommand(SQL, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isDependent = bool.Parse(reader["Relation"].ToString());
                }
                return isDependent;
            }
        }

        public String Description
        {
            get
            {
                string description;
                string querySQL = "select Description FROM TestTask WHERE id = " + Id;
                command = new SQLiteCommand(SQL, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    description = reader["Description"].ToString();
                }
                return description;
            }
        }
    }
}
