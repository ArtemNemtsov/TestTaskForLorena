using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.IO;

namespace TaskforLorena_work_with_DBSQLite_
{
    public partial class MainWindow : Window
    {
        readonly string connectDBPath = "..\\..\\";
        readonly string BDFileName = "TestBDForLorena.db";
        string cwd = System.IO.Directory.GetCurrentDirectory();

        public MainWindow()
        {
            InitializeComponent();
            fullPathToBD = System.IO.Path.Combine(connectDBPath, BDFileName);  //комбинируем полный путь БД
            BDStorage storage = new BDStorage(fullPathToBD);
            if (storage.DBTableExist("TestTable"))                // если таблицы нет, создаем ее
            {
                storage.LoadTabletoBD(storage.createTableSQL);
                int IdMiass = BDStorage.CreateCellsOffice("Миасс", 4, false, "", 0);        //заполняем таблицу в БД
                int IdAmelia = BDStorage.CreateCellsOffice("Амелия", 5, true, "", IdMiass);
                int IdTest1 = BDStorage.CreateCellsOffice("Тест1", 2, true, "", IdAmelia);
                int IdKurgan = BDStorage.CreateCellsOffice("Курган", 2, false, "", 0);
                int IdTest2 = BDStorage.CreateCellsOffice("Тест2", 0, true, "", IdKurgan);
            }
            var mainDeps = storage.GetMainDepartments();
            foreach (IDepartment dep in mainDeps)
            {
                printIDepartment(dep);
                var childDeps = dep.GetChildDepartments();
                Console.WriteLine("Children:");
                foreach (var child in childDeps)
                {
                    Console.Write("   ");  // Просто отступ
                    printIDepartment(child);

                    double price = 57470.0;
                    Console.WriteLine("   Price. Before discount: {0}. After discount: {1}", price, CalcDiscountedPrice(child, price));
                }
            }
        }


        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "qweerty";

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

/*
  cwd += connectDBPath;
  fullPathToBD = System.IO.Path.Combine(connectDBPath, BDFileName);  //комбинируем полный путь БД
  BDStorage BDStorage = new BDStorage(fullPathToBD);              // подключаемся к БД   
  if (BDStorage.IsExistTableStatus("TestTable"))                // если таблицы нет, создаем ее
  {
      BDStorage.LoadTabletoBD(BDStorage.createTableSQL);
  }

  List<string> ShopsName = BDStorage.GetShops(); // показываем список магазинов
  foreach (string shop in ShopsName)
  {
      Сhoice.Items.Add(shop);
  }
  */
