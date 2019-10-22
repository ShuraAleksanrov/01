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
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
namespace MainWS
{
    /// <summary>
    /// Логика взаимодействия для Tic.xaml
    /// </summary>
    public partial class Tic : Window
    { 
        MySqlConnection connection;
        public DataRowView row;
        public Tic()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection("server = localhost; database = ws; user = root; password = root;");
            connection.Open();
            DataTable table1 = new DataTable();
            MySqlCommand command = new MySqlCommand($"select * from tic where fk = {row.Row["id"].ToString()};", connection);
            MySqlDataReader reader = command.ExecuteReader();
            table1.Load(reader);
            TicDG.ItemsSource = table1.DefaultView;
            reader.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            connection.Close();
        }

        private void TicDG_Selected()
        {
            if (TicDG.SelectedIndex != -1)
            {
                DataRowView rowing = TicDG.SelectedItem as DataRowView;
                BitmapImage help = new BitmapImage();
                help.BeginInit();
                help.UriSource = new Uri(Environment.CurrentDirectory + @"\img\" + rowing.Row["img"]);
                help.EndInit();
                img.Source = help;
                video.Source = new Uri(Environment.CurrentDirectory + @"\video\" + rowing.Row["video"]);
            }
            else
            {
                MessageBox.Show("Не выбран штраф!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TicDG_Selected();
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DriverExp win = new DriverExp();
            win.Show();
            this.Close();
        }
    }
}
