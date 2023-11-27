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
    public partial class cardsForm : Form
    {
        public cardsForm()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password= ;database=dbkur");

        private void cardsForm_Load(object sender, EventArgs e)
        {
            connection.Open();

            string query = "SELECT FIO, address, card_id FROM guest JOIN booking USING(guest_id)";

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
