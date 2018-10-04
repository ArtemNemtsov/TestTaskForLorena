using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace Lorena
{
    public class Department : IDepartment
    {
        private readonly SQLiteConnection dbConnection;

        public int Id { get; private set; }
        public IDepartment Parent { get; private set;  }
        
        public Department(SQLiteConnection connection, int id, IDepartment parent = null)
        {
            this.Id = id;
            this.dbConnection = connection; 
            this.Parent = parent;
        }

        private SQLiteDataReader ExecuteSelectQuery(String query)
        {
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            return command.ExecuteReader();
        }

        public List<IDepartment> GetChildDepartments()
        {
            var children = new List<IDepartment>();                 //  идем в бд и по индексу this.id нахоидм id всех детей          
                                                       // всех детей складываем в список children 
            var reader = ExecuteSelectQuery("select id from TestTask where parent_id = " + Id);
            while (reader.Read())
            {
                int childId = int.Parse(reader[0].ToString());
                IDepartment childDepartment = new Department(dbConnection, childId, this);   // this- говорим кто родитель
                children.Add(childDepartment);
            }
            return children;
        }

        public String Name
        {
            get   // используя id и connecton получаем из бд имя 
            {
                var reader = ExecuteSelectQuery("select Name FROM TestTask WHERE id = " + Id);
                if (reader.Read())
                {
                    return reader[0].ToString();
                }

                return "<NONAME>";
            }
        }

        public Double Discount
        {
            get
            {
                var reader = ExecuteSelectQuery("select Discount FROM TestTask WHERE id = " + Id);
                if (reader.Read())
                {
                    return double.Parse((reader[0]).ToString());
                }
                return 0;
            }   
        }

        public Boolean IsDependent
        {
            get
            {
                var reader = ExecuteSelectQuery("select Relation FROM TestTask WHERE id = " + Id);
                while (reader.Read())
                {
                    return int.Parse(reader[0].ToString()) == 1;
                }
                return false;
            }
        }

        public String Description
        {
            get
            {
                var reader = ExecuteSelectQuery("select Description FROM TestTask WHERE id = " + Id);
                while (reader.Read())
                {
                    return reader[0].ToString();
                }
                return "";
            }
        }
    }
}
