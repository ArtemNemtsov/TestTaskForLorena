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

        List<IDepartment> allDeps;

        public AddDepartment(List<IDepartment> allDeps, DBStorage storage)
        {
            InitializeComponent();
            this.allDeps = allDeps;
            this.storage = storage;     
            CheckBoxDepends.IsChecked  =  false;
            TBoxDiscount.MaxLines = 1;
            TBoxDiscount.MaxLength = 6;
            sliderDiscount.Minimum = 0.0;
            sliderDiscount.Maximum = 100.0;
        }

        IDepartment GetParentFromList (List<IDepartment> deps)
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

        bool CheckFildIsEmpty()
        {
            if (name == null)
                return false;
            return true;
        }

        private void TBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = TBoxName.Text;
        }

        private void TBoxDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
          
            // TBoxDiscount.Text = (Double.Parse(TBoxDiscount.Text)).ToString("000.00");
            discount = float.Parse(TBoxDiscount.Text);
        }

        private void sliderDiscount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string maxLenghDiscount = "";
            maxLenghDiscount = sliderDiscount.Value.ToString();
            if (maxLenghDiscount.Length > 5)
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
            this.Close();
        }

        private void CreateShop_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFildIsEmpty())
            {
               storage.CreateDepartment(name, discount, depend, description, parent);
               MessageBox.Show("Магазин успешно создан !", name, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Заполните обязательные поля", " Внимание !", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }

        private void TBoxDiscount_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
