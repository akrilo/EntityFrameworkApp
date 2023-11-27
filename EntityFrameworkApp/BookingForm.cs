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
    public partial class BookingForm : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password= ;database=dbkur");

        public BookingForm()
        {
            InitializeComponent();
        }

        private void BookingForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbkurDataSet.room' table. You can move, or remove it, as needed.
            this.roomTableAdapter.Fill(this.dbkurDataSet.room);
            // TODO: This line of code loads data into the 'dbkurDataSet.guest' table. You can move, or remove it, as needed.
            this.guestTableAdapter.Fill(this.dbkurDataSet.guest);
            // TODO: This line of code loads data into the 'dbkurDataSet.booking' table. You can move, or remove it, as needed.
            this.bookingTableAdapter.Fill(this.dbkurDataSet.booking);

            connection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "INSERT INTO `guest`(`guest_id`, `fio`, `gender`, `address`) VALUES (" + "'" + textBox1.Text + "'" + "," + "'" + textBox2.Text + "'" + "," + "'" + comboBox2.SelectedItem + "'" + "," + "'" + textBox11.Text + "'" + ")";
            string query2 = "INSERT INTO `booking`(`guest_id`, `arrivalDate`, `departureDate`, `card_id`, regularDiscount, studentDiscount, seasonalDiscount) VALUES (" + "'" + textBox1.Text + "'" + "," + "'" + dateTimePicker1.Value.ToShortDateString() + "'" + "," + "'" + dateTimePicker2.Value.ToShortDateString() + "'" + "," + "'" + textBox13.Text + "'" + "," + "'" + Convert.ToInt32(checkBox1.Checked) + "'" + "," + "'" + Convert.ToInt32(checkBox3.Checked) + "'" + "," + "'" + Convert.ToInt32(checkBox2.Checked) + "'" + ")";

            if (connection.State == ConnectionState.Open)
            {
                MySqlCommand command1 = new MySqlCommand(query1, connection);
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                try
                {
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    MessageBox.Show("Постоялец зарегистрирован!");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    throw;
                }
                textBox1.Clear();
                textBox2.Clear();
                textBox13.Clear();
            }
            else
            {
                MessageBox.Show("Соединения с БД нет");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cardsForm f = new cardsForm();
            f.Show();
        }
    }
}

