﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text.RegularExpressions;


namespace Foreign_School
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            frMain.NavigationService.Navigate(new Forms.Info());
        }

       
        
        private void WindowExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            frMain.NavigationService.Navigate(new Forms.Clients());
            
        }

        
    }
}
