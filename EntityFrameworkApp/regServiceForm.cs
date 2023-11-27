using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using EntityFrameworkApp.Properties;
using MySql.Data.MySqlClient;

namespace EntityFrameworkApp
{
    public partial class regServiceForm : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password= ;database=dbkur");
        public regServiceForm()
        {
            InitializeComponent();
        }   
        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            int service_id = comboBox1.SelectedIndex + 1;
            string query1 = "INSERT INTO `servicerendered`(`serviceRendered_id`, `service_id`, `amount`) VALUES (" + "'" + textBox1.Text + "'" + "," + "'" + service_id + "'" + "," + "'" + textBox2.Text + "'" + ")";
            string query2 = "UPDATE `booking` SET `serviceRendered_id` = '" + textBox1.Text + "' , `paymentType` =  '" + comboBox3.SelectedItem + "' WHERE `card_id` = '" + comboBox2.SelectedItem + "'";

            if (connection.State == ConnectionState.Open)
            {
                MySqlCommand command1 = new MySqlCommand(query1, connection);
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                try
                {
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    MessageBox.Show("Услуга выбрана!");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    throw;
                }
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Соединения с БД нет");
            }

            dataGridView2.Rows.Clear();
            string query = "SELECT * FROM `servicerendered`";
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
                dataGridView2.Rows.Add(s);
        }
    }
}
