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
    public partial class workTypeForm : Form
    {
        public workTypeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guestForm f = new guestForm();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            amountForm f = new amountForm();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            regServiceForm f = new regServiceForm();
            f.Show();
        }
    }
}
