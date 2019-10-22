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
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Threading;
namespace MainWS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
        }
        int s = 0;
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"select * from user where name ='{name.Text.ToString()}' and password = '{password.Text.ToString()}'; ", connection);
            MySqlDataReader reader = command.ExecuteReader();
            Thread.Sleep(5);
            if (name.Text.ToString().Equals("") && password.Text.ToString().Equals(""))
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else {
                
                if (reader.Read())
                {
                    DriverExp win = new DriverExp();
                    win.Show();
                    this.Close();
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    if (s > 2)
                    {
                        
                        command = new MySqlCommand($"insert into error (pcname) values('{Environment.MachineName}')", connection);
                        MySqlDataReader reader2 = command.ExecuteReader();
                        MessageBox.Show("Несанкционированный вход. \nДанные о входе занесены в базу данных. \nПрограмма заблокированна на 5 секунд.");
                        Thread.Sleep(5000);
                        s = 0;
                        reader2.Close();
                    }

                    else
                    {
                        s++;
                        MessageBox.Show("Проверьте правильность введенных данных.");
                    }
                }
            }
            
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        { this.Close(); }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection("server = localhost; database = ws; user = root; password = root;");
            connection.Open();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            connection.Close();
        }
    }
}
