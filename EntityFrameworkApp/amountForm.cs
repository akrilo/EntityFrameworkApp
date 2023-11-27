using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkApp
{
    public partial class amountForm : Form
    {
        class BookingAmount
        {
            public BookingAmount() { }

            public BookingAmount(String name, int amount)
            {
                this.name = name;
                this.amount = amount;
            }

            public string name { get; set; }
            public int amount { get; set; }
        }
        class ServiceAmount
        {
            public ServiceAmount() { }

            public ServiceAmount(String name, int amount)
            {
                this.name = name;
                this.amount = amount;
            }

            public string name { get; set; }
            public int amount { get; set; }
        }
        public amountForm()
        {
            InitializeComponent();
        }
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password= ;database=dbkur");
        private void amountForm_Load(object sender, EventArgs e)
        {
            connection.Open();
            string query1 = "SELECT fio, price * DATEDIFF(departureDate,arrivalDate) as summ FROM booking JOIN guest USING(guest_id) JOIN card USING(card_id) JOIN room USING(room_id)";
            if (connection.State == ConnectionState.Open)
            {
                List<ServiceAmount> ServiceAmounts = new List<ServiceAmount>();
                try
                {
                    MySqlCommand command = new MySqlCommand(query1, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    //List<string[]> data = new List<string[]>();
                    while (reader.Read())
                    {
                        String name = "";
                        int amount = 0;
                        name = reader[0].ToString();
                        amount = Int32.Parse(reader[1].ToString());
                        ServiceAmount tempServiceAmount = new ServiceAmount(name, amount);
                        ServiceAmounts.Add(tempServiceAmount);
                    }
                    reader.Close();
                    SeriesCollection series = new SeriesCollection();
                    ChartValues<int> amounts = new ChartValues<int>();
                    List<string> names = new List<string>();
                    foreach (ServiceAmount ServiceAmount in ServiceAmounts)
                    {
                        names.Add(ServiceAmount.name);
                        amounts.Add(ServiceAmount.amount);
                    }
                    cartesianChart1.AxisX.Clear();
                    cartesianChart1.AxisX.Add(new Axis //Добавляем на ось X значения
                    {                                  //имён слишком много, поэтому показывает не всех
                        Title = "ФИО",
                        Labels = names
                    });
                    LineSeries line = new LineSeries(); //Создаем линию, задаем ей значения из коллекции
                    line.Title = "";
                    line.Values = amounts;
                    series.Add(line); //Добавляем линию на график
                    cartesianChart1.Series = series; //Отрисовываем график в интерфейсе
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    throw;
                }
            }
            string query2 = "SELECT fio, price * amount as summ FROM booking JOIN guest USING(guest_id) JOIN servicerendered USING(serviceRendered_id) JOIN service USING(service_id)";
            if (connection.State == ConnectionState.Open)
            {
                List<BookingAmount> BookingAmounts = new List<BookingAmount>();
                try
                {
                    MySqlCommand command = new MySqlCommand(query2, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    //List<string[]> data = new List<string[]>();
                    while (reader.Read())
                    {
                        String name = "";
                        int amount = 0;
                        name = reader[0].ToString();
                        amount = Int32.Parse(reader[1].ToString());
                        BookingAmount tempBookingAmount = new BookingAmount(name, amount);
                        BookingAmounts.Add(tempBookingAmount);
                    }
                    reader.Close();
                    SeriesCollection series = new SeriesCollection();
                    ChartValues<int> amounts = new ChartValues<int>();
                    List<string> names = new List<string>();
                    foreach (BookingAmount BookingAmount in BookingAmounts)
                    {
                        names.Add(BookingAmount.name);
                        amounts.Add(BookingAmount.amount);
                    }
                    cartesianChart2.AxisX.Clear();
                    cartesianChart2.AxisX.Add(new Axis //Добавляем на ось X значения
                    {                                  //имён слишком много, поэтому показывает не всех
                        Title = "ФИО",
                        Labels = names
                    });
                    LineSeries line = new LineSeries(); //Создаем линию, задаем ей значения из коллекции
                    line.Title = "";
                    line.Values = amounts;
                    series.Add(line); //Добавляем линию на график
                    cartesianChart2.Series = series; //Отрисовываем график в интерфейсе
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    throw;
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
