using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;
using System.IO;

namespace Lorena
{
    public partial class MainWindow : Window
    {
        private DBStorage storage;
        List <IDepartment> allDepartments;

        public MainWindow()
        {
            InitializeComponent();
        }
                                                                                     
        private static List<IDepartment> flattenDepartments(List<IDepartment> deps)      //метод вернет все оффисы(от родительских к дочерним) 
        {
            List<IDepartment> result = new List<IDepartment>();                  //создаем List
            foreach (var dep in deps)                                           // для каждого офисса из полученного списка оффисов делаем :
            {
                result.Add(dep);                                                // 1) каждый офиис добавляем в лист Result
                var children = flattenDepartments(dep.GetChildDepartments());   // получаем дочку и ее тоже передаем в аргументы flattenDepartments(дочка)
                foreach (var child in children)                                 //3) для каждого дочернего оффиса : 
                {
                    result.Add(child);                                           //Добавляем в лист Result дочку
                }
            }
            return result;                                                       // возвращаем лист result
        }

        static double CalcTotalDiscount(IDepartment dep)                          //вычисляем общую (родительскую) скидку для конкретного dep
        {
            double discount = dep.Discount;
            if (dep.IsDependent && dep.Parent != null && dep.Parent.Id > 0)     // если есть родительи его ID !=0, то 
            {
                discount += CalcTotalDiscount(dep.Parent);                      // скидка = скидка + (вычисляем скидку(родителя))
            }
            return discount;
        }

        double CalcDiscountedPrice(IDepartment dep, double price, bool logToDataBase = false)    // вычисляем цену для конкретного dep по формуле
        {
            double totalDiscount = CalcTotalDiscount(dep);                         //вычисляем родительскую скидку
            double discountedPrice = price - price * (totalDiscount / 100.0);      //скидка для нашего dep

            if (logToDataBase)                                                     //если (нажата клавиша "Записать в БД"), то
            {   
                double parentDiscount = totalDiscount - dep.Discount;
                storage.LogResult(dep, price, parentDiscount, discountedPrice);    //записываем результат в БД
            }
            return discountedPrice;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectDBPath = "..\\..\\";
            string BDFileName = "TestBDForLorena.db";
            string cwd = System.IO.Directory.GetCurrentDirectory();
            string fullPathToBD = "";
            fullPathToBD = System.IO.Path.Combine(connectDBPath, BDFileName);        //комбинируем полный путь   к БД
            storage = new DBStorage(fullPathToBD);

            storage.CreateRequiredTableIsNotExist();                                 //создаем таблицу, если онане существует...

            if (storage.TableIsEmpty("TestTask"))                                    // Если таблица пуста, заполняем ее данными ниже...
            {
                var IdMiass = storage.CreateDepartment("Миасс", 4, false, "");              //заполняем таблицу в БД
                var IdAmelia = storage.CreateDepartment("Амелия", 5, true, "", IdMiass);
                var IdTest1 = storage.CreateDepartment("Тест1", 2, true, "", IdAmelia);
                var IdTest2 = storage.CreateDepartment("Тест2", 0, true, "", IdMiass);
                var IdKurgan = storage.CreateDepartment("Курган", 11, false, "");
            }
            var mainDeps = storage.GetMainDepartments();                             // получаем главные офиссы
            allDepartments = flattenDepartments(mainDeps);                           // получаем  в list<Departament> все офисы

            AppendChildrenRecursively(treebase, mainDeps);                          
        }

        void AppendChildrenRecursively(ItemsControl treebase, List<IDepartment> ChildDeparts)       //метод показывает список магазинов в дереве TreeView
        {
            foreach (var dep in ChildDeparts)                  
            {
                TreeViewItem rootItem = new TreeViewItem();
                rootItem.Tag = dep;
                rootItem.Header = dep.Name;
                treebase.Items.Add(rootItem);
                AppendChildrenRecursively(rootItem, dep.GetChildDepartments());
            }
        }

        private void RecalcResult(bool logtodatabase = false)                              //вычисляем результат, по умолчанию logToDatabase = false (записи в БД не делаем)
        {
            TreeViewItem item = (TreeViewItem)treebase.SelectedItem;   // Свойство SelectedItem реализованное для TreeViewItem возвращает тип Object.

            IDepartment selecteddepartment = (IDepartment)item.Tag;

            double price;
            if (double.TryParse(textBox1.Text, out price) && selecteddepartment != null)
            {
                double discountedprice = CalcDiscountedPrice(selecteddepartment, price, logtodatabase);
                tbDiscountedPrice.Text = discountedprice.ToString();

                if (logtodatabase)
                {
                    MessageBox.Show("данные записаны", "результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            else if (logtodatabase)
            {
                MessageBox.Show("невозможно записать данные в бд. введите цену.", "ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnWriteDataClick(object sender, RoutedEventArgs e)
        {
            RecalcResult(true);                  //записать в БД - true
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecalcResult();
        }

        private void treebase_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            RecalcResult();
        }

        private void CloseProgramClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddDepartment depart = new AddDepartment(allDepartments);
            depart.ShowDialog();
        }
    }
}

