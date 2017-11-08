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
    public partial class statisticsForm : Form
    {
        public statisticsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            DestroyHandle();
        }

        private void statisticsForm_Load(object sender, EventArgs e)
        {
            using (var prodDB = new ProdContent())
            {
                label7.Text = prodDB.Customers.Count().ToString();
                label8.Text = prodDB.Category.Count().ToString();
                label9.Text = prodDB.Product.Count().ToString();
                label10.Text = prodDB.Orders.Count().ToString();
            }
        }
    }
}
