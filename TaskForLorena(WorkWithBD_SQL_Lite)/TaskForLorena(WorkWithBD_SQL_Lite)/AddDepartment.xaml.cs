﻿using System;
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
        IDepartment parent;
      
        List<IDepartment> allDeps;

        public AddDepartment(List<IDepartment> allDeps)
        {
            this.allDeps = allDeps;
            InitializeComponent();
            CheckBoxDepends.IsChecked  =  false;
        }

        private void TBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = TBoxName.Text;
        }

        private void TBoxDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            discount = float.Parse(TBoxDiscount.Text);
        }

        private void CheckBoxDepends_Checked(object sender, RoutedEventArgs e)
        {
                ComboBox_Parent.ItemsSource = allDeps;
                ComboBox_Parent.DisplayMemberPath = "Name";
        }

        private void CheckBoxDepends_Unchecked(object sender, RoutedEventArgs e)
        {
            ComboBox_Parent.ItemsSource = null;
        }

        private void TBoxDecription_TextChanged(object sender, TextChangedEventArgs e)
        {
            description = TBoxDecription.Text;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddDepartm_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateShop_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}