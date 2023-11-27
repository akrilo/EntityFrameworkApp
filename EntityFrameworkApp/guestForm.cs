using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using EntityFrameworkApp.Properties;
using MySql.Data.MySqlClient;

namespace EntityFrameworkApp
{
    public partial class guestForm : Form
    {
        double reg = 0, seas = 0, st = 0, sale = 0;
        public guestForm()
        {
            InitializeComponent();
        }
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password= ;database=dbkur");
        private void button1_Click(object sender, EventArgs e)
        {
            BookingForm f = new BookingForm();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\USER\\source\\repos\\EntityFrameworkApp\\Список постоянных клиентов.txt");
            string query = "SELECT fio, gender, address, card_id, COUNT(guest_id) as countg FROM booking JOIN guest USING(guest_id) GROUP BY fio, gender, address, card_id HAVING COUNT(guest_id) > 1 ";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[10]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString(); 
            }
            reader.Close();
            foreach (string[] s in data)
                sw.WriteLine(s[0] + "|" + s[1] + "|" + s[2] + "|" + s[3] + "|" + s[4]);
            sw.Close();
        }

        private void guestForm_Load(object sender, EventArgs e)
        {
            connection.Open();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\USER\\source\\repos\\EntityFrameworkApp\\Клиенты с бонусом.txt");
            string query = "SELECT fio, card_id, price * DATEDIFF(departureDate,arrivalDate) / 10 as bonus, price * DATEDIFF(departureDate,arrivalDate) as prices FROM booking JOIN guest USING(guest_id) JOIN card USING(card_id) JOIN room USING(room_id) HAVING prices >= 5000";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[10]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }
            reader.Close();
            foreach (string[] s in data)
                sw.WriteLine(s[0] + "|" + s[1] + "|" + s[2]);
            sw.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\USER\\source\\repos\\EntityFrameworkApp\\Список постояльцев, имеющих скидки.txt");
            string query = "SELECT fio, card_id, regularDiscount, studentDiscount, seasonalDiscount FROM booking JOIN guest USING(guest_id) JOIN card USING(card_id) JOIN room USING(room_id) HAVING regularDiscount = 1 OR studentDiscount = 1 OR seasonalDiscount = 1";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[10]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                if (Convert.ToInt32(reader[2]) == 1)
                    reg = 0.10;
                if (Convert.ToInt32(reader[3]) == 1)
                    seas = 0.30;
                if (Convert.ToInt32(reader[4]) == 1)
                    st = 0.20;
                sale = reg + seas + st;
                data[data.Count - 1][2] = sale.ToString();
            }
            reader.Close();
            foreach (string[] s in data)
                sw.WriteLine(s[0] + "|" + s[1] + "|" + s[2]);
            sw.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\USER\\source\\repos\\EntityFrameworkApp\\Счет.txt");
            string query1 = "SELECT price * DATEDIFF(departureDate, arrivalDate) as summ FROM booking JOIN guest USING(guest_id) JOIN card USING(card_id) JOIN room USING(room_id)";
            MySqlCommand command1 = new MySqlCommand(query1, connection);
            MySqlDataReader reader1 = command1.ExecuteReader();
            reader1.Read();
            double sum = Convert.ToDouble(reader1[0]);
            reader1.Close();
            string query2 = "SELECT price * amount as summ FROM booking JOIN guest USING(guest_id) JOIN servicerendered USING(serviceRendered_id) JOIN service USING(service_id)";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            MySqlDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            sum += Convert.ToDouble(reader2[0]) - Convert.ToDouble(reader2[0]) * sale;
            reader2.Close();
            string query3 = "UPDATE `booking` SET `sum` = '" + sum + "' WHERE `card_id` = '" + textBox13.Text + "'";
            MySqlCommand command3 = new MySqlCommand(query3, connection);
            command3.ExecuteNonQuery();
            string query4 = "SELECT fio, card_id, sum FROM booking JOIN guest USING(guest_id) WHERE `card_id` = '" + textBox13.Text + "'";
            MySqlCommand command4 = new MySqlCommand(query4, connection);
            MySqlDataReader reader4 = command4.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader4.Read())
            {
                data.Add(new string[10]);
                data[data.Count - 1][0] = reader4[0].ToString();
                data[data.Count - 1][1] = reader4[1].ToString();
                data[data.Count - 1][2] = reader4[2].ToString();
            }
            reader4.Close();
            foreach (string[] s in data)
                sw.WriteLine(s[0] + "|" + s[1] + "|" + s[2]);
            sw.Close();
        }     
    }
}
