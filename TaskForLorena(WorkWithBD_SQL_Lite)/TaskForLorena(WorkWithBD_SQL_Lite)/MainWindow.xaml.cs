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

//office = s.CreateSellsOffice(miass)
//child = office.AddChildOffice(amelia)
//child.AddChildOffice(test1)
//office.AddChildOffice(test2)
//s.CreateSellsOffice(kurgan)
//miassOffice = s.createSellsOffice("Miass", "", 32, 0);
//amiliaOffice = s.createSellsOffice("Amelia", "", 12, 1, miassOffice);
//test1Office = s.createSells("Test1", "", 11, 1, ameliaOffice);

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
            IBDStorage BDStorage = new BDStorage(fullPathToBD);              // подключаемся к БД         
            int IdMiass =  BDStorage.CreateCellsOffice("Миасс", 4,  false, "", 0);     //заполняем таблицу в БД
            int IdAmelia = BDStorage.CreateCellsOffice("Амелия", 5, true, "", IdMiass);
            int IdTest1 = BDStorage.CreateCellsOffice("Тест1", 2, true, "", IdAmelia);          
            int IdKurgan =  BDStorage.CreateCellsOffice("Курган", 2, false, "", 0);
            int IdTest2 = BDStorage.CreateCellsOffice("Тест2", 0, true, "", IdKurgan);

            //Сhoice.Items.Add();
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
