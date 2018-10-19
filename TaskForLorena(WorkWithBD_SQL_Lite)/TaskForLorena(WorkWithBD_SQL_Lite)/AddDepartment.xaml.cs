using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lorena
{
    public partial class AddDepartment : Window
    {
        string name = "";
        float discount = 0;
        bool depend = false;
        string description = "";
        IDepartment parent = null;
        DBStorage storage;
        private static readonly Regex _regex = new Regex("[^0-9.]");
        List<IDepartment> allDeps;
        MainWindow mainWin = new MainWindow();  //создаем экземпляр класса MainWindow

        public AddDepartment(List<IDepartment> allDeps, DBStorage storage)
        {
            InitializeComponent();
            this.allDeps = allDeps;
            this.storage = storage;
            CheckBoxDepends.IsChecked = false;
            TBoxDiscount.MaxLines = 1;
            TBoxDiscount.MaxLength = 6;
            sliderDiscount.Minimum = 0.0;
            sliderDiscount.Maximum = 100.0;
        }

        IDepartment GetParentFromList(List<IDepartment> deps)   //метод возврщает родителя 
        {
            foreach (var dep in deps)
            {
                if (ComboBox_Parent.SelectedValue == dep)
                {
                    return dep;
                }
            }
            return null;
        }

        bool CheckFildIsEmpty()                            //метод проверяет , не пустое ли имя. т.к. оно обязательное поле 
        {
            if (name == "")
                return true;
            return false;
        }

        private void TBoxName_TextChanged(object sender, TextChangedEventArgs e)    
        {
            name = TBoxName.Text;
        }

        private void sliderDiscount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string maxLenghDiscount = "";
            maxLenghDiscount = sliderDiscount.Value.ToString();
            if (maxLenghDiscount.Length > 5)                  //ограничиваем длину поля Discount не больше 5 
            {
                maxLenghDiscount = maxLenghDiscount.Remove(5);
            }
            TBoxDiscount.Text = maxLenghDiscount;
        }

        private void CheckBoxDepends_Checked(object sender, RoutedEventArgs e)
        {
            depend = true;
            ComboBox_Parent.ItemsSource = allDeps;
            ComboBox_Parent.DisplayMemberPath = "Name";
        }

        private void CheckBoxDepends_Unchecked(object sender, RoutedEventArgs e)
        {
            depend = false;
            ComboBox_Parent.ItemsSource = null;
        }

        private void TBoxDecription_TextChanged(object sender, TextChangedEventArgs e)
        {
            description = TBoxDecription.Text;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            parent = GetParentFromList(allDeps);
        }

        private void AddDepartm_back_Click(object sender, RoutedEventArgs e)
        {
            mainWin.ShowDialog();
            this.Close();
        }

        private void CreateShop_Click(object sender, RoutedEventArgs e)          //при нажатие клавиши создать магазин 
        {
            if (!CheckFildIsEmpty())                                         //если поле Имя не пустое, то записываем в БД 
            {
                storage.CreateDepartment(name, discount, depend, description, parent);
                MessageBox.Show("Магазин успешно создан !", name, MessageBoxButton.OK, MessageBoxImage.Information);                
                this.Close();
                mainWin.ShowDialog();
            }
            else MessageBox.Show("Заполните обязательные поля", " Внимание !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private static bool IsTextAllowed(string text)           //метод проверяет, совпадения в  regex и строки из аргумента
        {
            return !_regex.IsMatch(text);
        }

        private void TBoxDiscount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextAllowed(e.Text))
            {
                e.Handled = true;
            }
            else if (e.Text == ".") discount += 0.0F;
            else discount += float.Parse(e.Text);
        }

        private void TBox_Discount_Changed (object sender, TextChangedEventArgs e)
        {
            float discountTxbValue;
            
            if (float.TryParse(TBoxDiscount.Text, out discountTxbValue))
            {
                if (discountTxbValue > 100.00)                //ограничиваем ввод не больше 100 %
                {
                    TBoxDiscount.Text = "100";
                }
                if (TBoxDiscount.Text.Length > 5)           //ограничиваем числов до  5 символов  в длину 
                {
                    TBoxDiscount.Text = TBoxDiscount.Text.Remove(5);
                }
            }           
        }
    }
}



    

