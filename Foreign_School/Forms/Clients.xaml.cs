using System;
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
 


namespace Foreign_School.Forms
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
      
        public Clients()
        {
            InitializeComponent();
        }
        public void Update_grid()
        {
            DataContext dc = new DataContext(Properties.Settings.Default.z);
            var d = from d1 in dc.GetTable<Class.Clients>()
                    where d1.status == true
                    select d1;
            dgClients.ItemsSource = d;

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Forms.Clients_red());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext dc = new DataContext(Properties.Settings.Default.z);
            Update_grid();
            Table<Class.Clients> clients = dc.GetTable<Class.Clients>();
            foreach (Class.Clients a in clients)
            {
                a.Photo = "/" + a.Photo;
            }

            dgClients.ItemsSource = clients;
        }

        private void WindowExit_Click(object sender, RoutedEventArgs e)
        {
            
            Application.Current.MainWindow.Close();
        }
    }
}
