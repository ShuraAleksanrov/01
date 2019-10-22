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
    /// Логика взаимодействия для LicBS.xaml
    /// </summary>
    public partial class LicBS : Window
    {
        MySqlConnection connection;
        public DataRowView row;
        public LicBS()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            connection.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection("server = localhost; database = ws; user = root; password = root;");
            connection.Open();
            MySqlCommand command = new MySqlCommand($"select * from lic where fk = {row.Row["id"].ToString()};", connection);
            MySqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            
            table.Load(reader);
            table.Columns.Add("status");
            for(int i = 0; i<table.Rows.Count; i++)
            {
                if (table.Rows[i]["statusB"].ToString().Equals("1"))
                {
                    table.Rows[i]["status"] = "Активен";
                }
                else table.Rows[i]["status"] = "Неактивен";
            }
            LicDG.ItemsSource = table.DefaultView;
            reader.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Lic win = new Lic();
            win.row = row;
            win.Show();
            this.Close();
        }


        private void Status_Click(object sender, RoutedEventArgs e)
        {
            
            if (LicDG.SelectedIndex != -1) { 
                DataRowView rowView = LicDG.SelectedItem as DataRowView;
                if (rowView.Row["statusB"].ToString().Equals("1"))
                {
                    MySqlCommand command1 = new MySqlCommand($"UPDATE `ws`.`lic` SET `statusB` = '0' WHERE (`id` = '{rowView["id"].ToString()}');", connection);
                    command1.ExecuteScalar();
                }
                else
                {
                    MySqlCommand command1 = new MySqlCommand($"UPDATE `ws`.`lic` SET `statusB` = '1' WHERE (`id` = '{rowView["id"].ToString()}');", connection);
                    command1.ExecuteScalar();
                }
            }
            else
            {
                MessageBox.Show("Не выбран водитель!");
            }
            MySqlCommand command = new MySqlCommand($"select * from lic where fk = {row.Row["id"].ToString()};", connection);
            MySqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            LicDG.ItemsSource = table.DefaultView;
            reader.Close();

            connection = new MySqlConnection("server = localhost; database = ws; user = root; password = root;");
            connection.Open();
            MySqlCommand command3 = new MySqlCommand($"select * from lic where fk = {row.Row["id"].ToString()};", connection);
            MySqlDataReader reader3 = command.ExecuteReader();
            DataTable table3 = new DataTable();

            table.Load(reader);
            table.Columns.Add("status");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["statusB"].ToString().Equals("1"))
                {
                    table.Rows[i]["status"] = "Активен";
                }
                else table.Rows[i]["status"] = "Неактивен";
            }
            LicDG.ItemsSource = table.DefaultView;
            reader.Close();
        }
    }
}
