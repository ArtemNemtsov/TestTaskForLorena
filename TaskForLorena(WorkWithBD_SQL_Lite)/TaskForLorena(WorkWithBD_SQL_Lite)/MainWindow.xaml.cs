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
        string fullPathToBD ;    
        string cwd = System.IO.Directory.GetCurrentDirectory();
             
        public MainWindow()
        {
            InitializeComponent();            
            cwd += connectDBPath;
            fullPathToBD = System.IO.Path.Combine(connectDBPath, BDFileName);  //комбинируем полный путь БД
            BDStorage BDStorage = new BDStorage(fullPathToBD);              // подключаемся к БД   
            if (BDStorage.IsExistTableStatus("TestTable"))                // если таблицы нет, создаем ее
            {
                BDStorage.LoadTabletoBD(BDStorage.createTableSQL);             
            }

             List<string> ShopsName = BDStorage.GetShops(); // показываем список магазинов
             foreach(string shop in ShopsName)
             {
                 Сhoice.Items.Add(shop);
             }

            /*int IdMiass =  BDStorage.CreateCellsOffice("Миасс", 4,  false, "", 0);     //заполняем таблицу в БД
              int IdAmelia = BDStorage.CreateCellsOffice("Амелия", 5, true, "", IdMiass);
              int IdTest1 = BDStorage.CreateCellsOffice("Тест1", 2, true, "", IdAmelia);          
              int IdKurgan =  BDStorage.CreateCellsOffice("Курган", 2, false, "", 0);
              int IdTest2 = BDStorage.CreateCellsOffice("Тест2", 0, true, "", IdKurgan);
              */


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
