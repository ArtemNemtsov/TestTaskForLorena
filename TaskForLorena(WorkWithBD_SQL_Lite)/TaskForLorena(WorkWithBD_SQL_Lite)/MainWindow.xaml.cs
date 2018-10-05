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

namespace Lorena
{
    public partial class MainWindow : Window
    {
        private DBStorage storage;

        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private static List<IDepartment> flattenDepartments(List<IDepartment> deps)
        {
            List<IDepartment> result = new List<IDepartment>();
            foreach (var dep in deps)
            {
                result.Add(dep);
                var children = flattenDepartments(dep.GetChildDepartments());
                foreach (var child in children)
                {
                    result.Add(child);
                }
            }
            return result;
        }

        static double CalcTotalDiscount(IDepartment dep)
        {
            double discount = dep.Discount;
            if (dep.IsDependent && dep.Parent != null && dep.Parent.Id > 0)
            { 
                discount += CalcTotalDiscount(dep.Parent);
            }
            return discount;
        }

        double CalcDiscountedPrice(IDepartment dep, double price, bool logToDataBase = false)
        {
            double totalDiscount = CalcTotalDiscount(dep);
            double discountedPrice = price - price * (totalDiscount / 100.0);

            if (logToDataBase)
            {
                double parentDiscount = totalDiscount - dep.Discount;
                storage.LogResult(dep, price, parentDiscount, discountedPrice);
            }
            return discountedPrice;
        }

   
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RecalcResult(true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectDBPath = "..\\..\\";
            string BDFileName = "TestBDForLorena.db";
            string cwd = System.IO.Directory.GetCurrentDirectory();
            string fullPathToBD = "";
            fullPathToBD = System.IO.Path.Combine(connectDBPath, BDFileName);  //комбинируем полный путь   к БД
            storage = new DBStorage(fullPathToBD);
            
            storage.CreateRequiredTableIsNotExist();
           
            if (storage.TableIsEmpty("TestTask"))  // Заполняем если в таблице нет записей
            {
                var IdMiass = storage.CreateDepartment("Миасс", 4, false, "");        //заполняем таблицу в БД
                var IdAmelia = storage.CreateDepartment("Амелия", 5, true, "", IdMiass);
                var IdTest1 = storage.CreateDepartment("Тест1", 2, true, "", IdAmelia);               
                var IdTest2 = storage.CreateDepartment("Тест2", 0, true, "", IdMiass);
                var IdKurgan = storage.CreateDepartment("Курган", 11, false, "");
            }

            
            var mainDeps = storage.GetMainDepartments();
            var allDepartments = flattenDepartments(mainDeps);

            cbDepartment.ItemsSource = allDepartments;
            cbDepartment.DisplayMemberPath = "Name";
            cbDepartment.SelectedIndex = 0;
        }

        private void RecalcResult(bool logToDatabase = false)
        {
            IDepartment selectedDepartment = (IDepartment)cbDepartment.SelectedItem;
            
            double price;
            if (Double.TryParse(textBox1.Text, out price) && selectedDepartment != null)
            {
                double discountedPrice = CalcDiscountedPrice(selectedDepartment, price, logToDatabase);
                tbDiscountedPrice.Text = discountedPrice.ToString();

                if (logToDatabase)
                {
                    MessageBox.Show("Данные записаны", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            else if (logToDatabase)
            {
                MessageBox.Show("Невозможно записать данные в БД. Введите цену.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecalcResult();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecalcResult();
        }
    }
}

/*
  List<string> ShopsName = BDStorage.GetShops(); // показываем список магазинов
  foreach (string shop in ShopsName)
  {
      Сhoice.Items.Add(shop);
  }
  */
