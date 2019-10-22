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
    /// Логика взаимодействия для Lic.xaml
    /// </summary>
    public partial class Lic : Window
    {
        MySqlConnection connection;
        public DataRowView row;
        public Lic()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DriverExp win = new DriverExp();
            win.Show();
            this.Close();
        }

        private void LicBS_Click(object sender, RoutedEventArgs e)
        {
            LicBS win = new LicBS();
            win.row = row;
            win.Show();
            this.Close(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection("server = localhost; database = ws; user = root; password = root;");
            connection.Open();
            lname.Text = row.Row["lname"].ToString();
            name.Text = row.Row["fname"].ToString() + " " + row.Row["mname"].ToString();
            string[] date = new string[3];
            date[0] = $"{row.Row["birth"].ToString()[6]}{row.Row["birth"].ToString()[7]}{row.Row["birth"].ToString()[8]}{row.Row["birth"].ToString()[9]}" ;
            date[1] = $"{row.Row["birth"].ToString()[3]}{row.Row["birth"].ToString()[4]}";
            date[2] = $"{row.Row["birth"].ToString()[0]}{row.Row["birth"].ToString()[1]}";;
            //row.Row["birth"].ToString().Remove(11).Split();
            // Debug.WriteLine($"\n%$%$%#^@^@^$%@${date[0]}.{date[0]}.{date[0]}\n");
            birthNregiog.Text = $"{date[0]}.{date[1]}.{date[2]}\n"+"Удмуртская респ.";
            BitmapImage helpimg = new BitmapImage();
            helpimg.BeginInit();
            helpimg.UriSource = new Uri($"{Environment.CurrentDirectory}" + @"\img\" + row.Row["img"].ToString() );
            helpimg.EndInit();
            img.Source = helpimg;
            MySqlCommand command = new MySqlCommand($"select * from lic where fk = {row.Row["id"].ToString()}" , connection);
            MySqlDataReader read = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(read);
            date[0] = $"{table.Rows[0]["dateS"].ToString()[6]}{table.Rows[0]["dateS"].ToString()[7]}{table.Rows[0]["dateS"].ToString()[8]}{table.Rows[0]["dateS"].ToString()[9]}";
            date[1] = $"{table.Rows[0]["dateS"].ToString()[3]}{table.Rows[0]["dateS"].ToString()[4]}";
            date[2] = $"{table.Rows[0]["dateS"].ToString()[0]}{table.Rows[0]["dateS"].ToString()[1]}";
            dateS.Text = $"{date[0]}.{date[1]}.{date[2]}";
            date[0] = $"{int.Parse(date[0])+10}";
            dateE.Text = $"{date[0]}.{date[1]}.{date[2]}";
            GIBDD.Text = table.Rows[0]["dep"].ToString();
            serial.Text = table.Rows[0]["num"].ToString() +" "+ table.Rows[0]["ser"].ToString();
            region.Text = "Удмуртская респ.";
            int i = 0; int q = table.Rows.Count;
            for (; i < q; i++)
            {
                if (table.Rows[i]["statusB"].ToString().Equals("1")) { 
                    if ((i + 1) == q)
                     {
                    kategory.Text += table.Rows[i]["class"].ToString();
                     }
                    else kategory.Text += table.Rows[i]["class"].ToString() + ", ";
                }
            }
            read.Close();
            //MySqlCommand sqlCommand = new MySqlCommand($"select * from reg where id ='{table.Rows[0]["region"]}'", connection);
            //MySqlDataReader myread = sqlCommand.ExecuteReader();
            //DataTable table1 = new DataTable();
            //table1.Load(myread);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            connection.Close();
        }       
    }
}
