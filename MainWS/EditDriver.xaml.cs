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
using Microsoft.Win32;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace MainWS
{
    /// <summary>
    /// Логика взаимодействия для EditDriver.xaml
    /// </summary>
    public partial class EditDriver : Window
    {
        MySqlConnection connection;
        public DataRowView dataRow;
        string image;
        public EditDriver()
        {
            InitializeComponent();
        }
        public string FileDir()
        {

            return null;
        }
        private void Img_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imageOF = new OpenFileDialog();
            imageOF.Filter = "PNG|*.png";
            imageOF.InitialDirectory = Environment.CurrentDirectory + @"\img\";
            if (imageOF.ShowDialog().HasValue)
            {
                string i = Environment.CurrentDirectory + @"\img\";
                image = imageOF.FileName;
                image = image.Remove(0, i.Length);
                Debug.WriteLine(image);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DriverExp win = new DriverExp();
            win.Show();
            this.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // Debug.WriteLine($"update ws.driver set lname = '{lname.Text}', fname = '{fname.Text}', mname = '{mname.Text}'," +
            //  $" ps = '{ps.Text}', pn = '{pn.Text}', pc = '{pc.Text}'," +
            // $" address = '{address.Text}', addressl = '{addressl.Text}', job = '{job.Text}'," +
            // $" company = '{company.Text}', phone = '{phone.Text}', email = '{email.Text}', img  = '{image}' WHERE id = {dataRow["id"].ToString()};");
            MySqlCommand command = new MySqlCommand($"update driver set lname = '{lname.Text}', fname = '{fname.Text}', mname = '{mname.Text}'," +
                $" ps = '{ps.Text}', pn = '{pn.Text}', pc = '{pc.Text}'," +
                $" address = '{address.Text}', addressl = '{addressl.Text}', job = '{job.Text}'," +
                $" company = '{company.Text}', phone = '{phone.Text}', email = '{email.Text}', birth = '{birth.Text}', img ='{image}' WHERE id = '{dataRow["id"].ToString()}'", connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Успешно");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            image = dataRow["img"].ToString();
            connection = new MySqlConnection("server = localhost; database = ws; user = root; password = root;");
            connection.Open();
            lname.Text = dataRow["lname"].ToString();
            fname.Text = dataRow["fname"].ToString();
            mname.Text = dataRow["mname"].ToString();
            ps.Text = dataRow["ps"].ToString();
            pn.Text = dataRow["pn"].ToString();
            pc.Text = dataRow["pc"].ToString();
            address.Text = dataRow["address"].ToString();
            addressl.Text = dataRow["addressl"].ToString();
            job.Text = dataRow["job"].ToString();
            company.Text = dataRow["company"].ToString();
            phone.Text = dataRow["phone"].ToString();
            email.Text = dataRow["email"].ToString();
            birth.Text = dataRow["birth"].ToString();
            //Debug.WriteLine($"^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n update ws.driver set lname = '{lname.Text}', fname = '{fname.Text}', mname = '{mname.Text}'," +
     //$" ps = '{ps.Text}', pn = '{pn.Text}', pc = '{pc.Text}'," +
    // $" address = '{address.Text}', addressl = '{addressl.Text}', job = '{job.Text}'," +
    // $" company = '{company.Text}', phone = '{phone.Text}', email = '{email.Text}', img  = '{image}' WHERE id = {dataRow["id"].ToString()};");

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            connection.Close();
        }
    }
}
