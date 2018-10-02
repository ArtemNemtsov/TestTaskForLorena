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

namespace TaskforLorena_work_with_DBSQLite_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Сhoice.Items.Add("ywafw");
            Сhoice.Items.Add("twafw");
            Сhoice.Items.Add("rwafw");
            Сhoice.Items.Add("ewafw");
            Сhoice.Items.Add("wwafw");
            Сhoice.Items.Add("qwafw");

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
