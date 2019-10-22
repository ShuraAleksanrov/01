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
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.IO;
namespace MainWS
{
    /// <summary>
    /// Логика взаимодействия для DriverExp.xaml
    /// </summary>
    public partial class DriverExp : Window
    {
        DataTable table = new DataTable();
        MySqlConnection connection;
        public DriverExp()
        {
            InitializeComponent();
        }

        private void EditDriver_Click(object sender, RoutedEventArgs e)
        {
            if(DriverDG.SelectedIndex != -1)
            {
                int i = DriverDG.SelectedIndex;
                EditDriver win = new EditDriver();
                win.dataRow = DriverDG.SelectedItem as DataRowView;
                win.Show();
                this.Close();
            }else
            {
                MessageBox.Show("Не выбран водитель!");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void Window_Closed(object sender, EventArgs e)
        {
            connection.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection("server = localhost; database = ws; user = root; password = root;");
            connection.Open();
            MySqlCommand command = new MySqlCommand("select * from driver;", connection);
            MySqlDataReader reader = command.ExecuteReader();            
            table.Load(reader);
            table.Columns.Add("image");
            int i = 0; int q = table.Rows.Count;
            for (;i<q;i++) {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(Environment.CurrentDirectory + @"\img\" + table.Rows[i]["img"]);
                image.EndInit();
                table.Rows[i]["image"] = image;
            }
            DriverDG.ItemsSource = table.DefaultView;
            reader.Close();
        }

        private void LicOpen_Click(object sender, RoutedEventArgs e)
        {
            if (DriverDG.SelectedIndex != -1)
            {
                Lic win = new Lic();
                win.row = DriverDG.SelectedItem as DataRowView;
                win.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Не выбран водитель!");
            }
            
        }

        private void TicOpen_Click(object sender, RoutedEventArgs e)
        {
            if (DriverDG.SelectedIndex != -1)
            {
                Tic win = new Tic();
                win.row = DriverDG.SelectedItem as DataRowView;
                win.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Не выбран водитель!");
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter fs = File.CreateText(Environment.CurrentDirectory + @"\driver.csv");
            fs.WriteLine(table.Columns[0].ToString() + ';' + table.Columns[1].ToString() + ';' + table.Columns[2].ToString() + ';' + table.Columns[3].ToString() + ';' + table.Columns[4].ToString() + ';' + table.Columns[5].ToString() + ';' + table.Columns[6].ToString() + ';' + table.Columns[7].ToString() + ';' + table.Columns[8].ToString() + ';' + table.Columns[9].ToString() + ';' + table.Columns[10].ToString() + ';' + table.Columns[11].ToString() + ';' + table.Columns[12].ToString() + ';' + table.Columns[13].ToString() + ';' + table.Columns[14].ToString() + ';');
            for(int i = 0; i<table.Rows.Count; i++)
            {
                for(int j = 0; j<table.Columns.Count; j++)
                {
                    if (!((j + 1) == table.Columns.Count)) { 
                    fs.Write(table.Rows[i][j] + ";");
                    }
                    else fs.WriteLine("");
                }
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (!((j + 1) == table.Columns.Count))
                    {
                        Debug.Write(table.Rows[i][j] + ";");
                    }
                    else Debug.WriteLine(table.Rows[i][j]);
                }
            }

            MessageBox.Show("Вывод успешен");
            fs.Close();
        }
    }
}
