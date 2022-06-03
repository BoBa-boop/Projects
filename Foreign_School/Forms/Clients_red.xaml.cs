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
using System.Text.RegularExpressions;

namespace Foreign_School.Forms
{
    /// <summary>
    /// Логика взаимодействия для Clients_red.xaml
    /// </summary>
    public partial class Clients_red : Page
    {
        public int sex;
        public bool e1 = false;//email неверен
        public Clients_red()
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update_grid();
        }

        private void tg_s_Checked(object sender, RoutedEventArgs e)
        {
            sex = 1;
        }

        private void tg_s_Unchecked(object sender, RoutedEventArgs e)
        {
            sex = 0;
        }

        private void date_MouseLeave(object sender, MouseEventArgs e)
        {
            if (date.SelectedDate != null)
            {
                txtBirth.Text = date.SelectedDate.Value.ToShortDateString();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)//удаление
        {
            try
            {
                DataContext dc = new DataContext(Properties.Settings.Default.z);
                var item = (Class.Clients)dgClients.SelectedItem;
                long vb = Convert.ToInt64((dgClients.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                Table<Class.Clients> cl = dc.GetTable<Class.Clients>();
                Class.Clients cls = cl.FirstOrDefault(clients => clients.Id.Equals(vb));
                cls.status = false;
                dc.SubmitChanges();
                MessageBox.Show("Ученик удален");
                Update_grid();
            }
            catch
            {
                MessageBox.Show("Выберите ученика из таблицы!");
            }
        }

        public void Val_email()
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            string email = txtEmail.Text;

            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                e1 = true;
            }
            else
            {
                MessageBox.Show("Некорректный email");
            }
        }//проверка пароля

        private void btnAdd_Click(object sender, RoutedEventArgs e)//Добавление
        {
            try
            {
                Val_email();

                if (e1 == true)
                {
                    DataContext dc = new DataContext(Properties.Settings.Default.z);
                    if (sex == 1)
                    {
                        Class.Clients newCl = new Class.Clients { Name = txtName.Text, LastName=txtLastName.Text, Otchestvo=txtOtch.Text, Birthday = Convert.ToDateTime(txtBirth.Text), Phone = txtPh.Text, Email = txtEmail.Text, Sex = "Женщина", status = true };
                        MessageBoxResult res = MessageBox.Show($"Вы добавите:\r ФИО:{txtLastName.Text} {txtName.Text} {txtOtch.Text}\rДень рождения{txtBirth.Text}\rТелефон:{txtPh.Text}\rEmail:{txtEmail.Text}\rПол:Женский\rПродолжить?", "Сообщение", MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.Yes)
                        {
                            dc.GetTable<Class.Clients>().InsertOnSubmit(newCl);
                            dc.SubmitChanges();
                        }
                    }
                    else if (sex == 0)
                    {
                        Class.Clients newCl = new Class.Clients { Name = txtName.Text, LastName = txtLastName.Text, Otchestvo = txtOtch.Text, Birthday = Convert.ToDateTime(txtBirth.Text), Phone = txtPh.Text, Email = txtEmail.Text, Sex = "Мужчина", status = true };
                        MessageBoxResult res = MessageBox.Show($"Вы добавите:\r ФИО:{txtLastName.Text} {txtName.Text} {txtOtch.Text}\rДень рождения{txtBirth.Text}\rТелефон:{txtPh.Text}\rEmail:{txtEmail.Text}\rПол:Мужской\rПродолжить?", "Сообщение", MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.Yes)
                        {
                            dc.GetTable<Class.Clients>().InsertOnSubmit(newCl);
                            dc.SubmitChanges();
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch
            {
                MessageBox.Show("Неправильный ввод данных");
            }
        }



        private void txtPh_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9()+ ]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtFIO_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zа-яА-Я -]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtBirth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)//Редактирование
        {

            try
            {
                if (txtBirth.Text == "" || txtEmail.Text == "" || txtName.Text == "" || txtLastName.Text == "" || txtOtch.Text == "" || txtPh.Text == "")
                {
                    MessageBox.Show("Для редактирования нужно выбрать данные из таблицы!");
                }
                else
                {
                    DataContext dc = new DataContext(Properties.Settings.Default.z);
                    Table<Class.Clients> cl = dc.GetTable<Class.Clients>();

                    var item1 = (Class.Clients)dgClients.SelectedItem;
                    long vb1 = Convert.ToInt64((dgClients.SelectedCells[0].Column.GetCellContent(item1) as TextBlock).Text);
                    Class.Clients cl1 = cl.FirstOrDefault(id => id.Id.Equals(vb1));
                    cl1.Name = txtName.Text;
                    cl1.LastName = txtLastName.Text;
                    cl1.Otchestvo = txtOtch.Text;
                    cl1.Birthday = Convert.ToDateTime(txtBirth.Text);
                    cl1.Email = txtEmail.Text;
                    cl1.Phone = txtPh.Text;
                    dc.SubmitChanges();
                    Update_grid();
                    MessageBox.Show("Данные изменены");

                }
            }
            catch
            {
                MessageBox.Show("Неверный ввод данных");
            }
        }



        private void btnOut_Click(object sender, RoutedEventArgs e)//Вывод в TextBox
        {
            try
            {
                DataContext dc = new DataContext(Properties.Settings.Default.z);
                var item = (Class.Clients)dgClients.SelectedItem;
                int vb = Convert.ToInt32((dgClients.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                Table<Class.Clients> cl = dc.GetTable<Class.Clients>();
                if (item.Id == vb)
                {
                    txtName.Text = item.Name;
                    txtLastName.Text = item.LastName;
                    txtOtch.Text = item.Otchestvo;
                    txtBirth.Text = item.Birthday.ToShortDateString();
                    txtEmail.Text = item.Email;
                    txtPh.Text = item.Phone;
                    if (item.Sex == "м")
                    {
                        tg_s.IsChecked = false;
                    }
                    else if (item.Sex == "ж")
                    {
                        tg_s.IsChecked = true;

                    }
                }
            }
            catch
            {
                MessageBox.Show("Выберите ученика из таблицы");
            }
        }

        private void WindowExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Forms.Clients());
        }

        private void btnDr_Click(object sender, RoutedEventArgs e)
        {
            DataContext dc = new DataContext(Properties.Settings.Default.z);
            var birt = from b in dc.GetTable<Class.Clients>()
                       where b.Birthday.Month == DateTime.Now.Month
                       select b;
            dgClients.ItemsSource = birt;
        }
    }
}
