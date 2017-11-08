using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class findClientOrdersForm : Form
    {
        public findClientOrdersForm()
        {
            InitializeComponent();

            using (var prodDB = new ProdContent())
            {
                var customers = from c in prodDB.Customers
                                orderby c.CompanyName
                                select c.CompanyName;

                foreach (var c in customers)
                {
                    comboBox1.Items.Add(c);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            DestroyHandle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var prodDB = new ProdContent())
            {
                var orders = prodDB.Customers.Include("Orders")
                    .Where(c => c.CompanyName == comboBox1.SelectedItem.ToString())
                    .Select(c => c.Orders).ToList();

                foreach(List<Order> o in orders)
                {
                    dataGridView1.DataSource = o;
                }
                dataGridView1.Update();
            }
        }
    }
}
