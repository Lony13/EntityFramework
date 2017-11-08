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

namespace Lab1
{
    public partial class StartForm : Form
    {
        ProdContent prodDB;

        public StartForm()
        {
            InitializeComponent();
            prodDB = new ProdContent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            prodDB.Category.Load();
            categoryBindingSource.DataSource = prodDB.Category.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewOrder newOrderForm = new NewOrder();
            newOrderForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewCustomerForm newCustomer = new NewCustomerForm();
            newCustomer.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            newCategoryForm newCategory = new newCategoryForm();
            newCategory.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            newProductForm newProduct = new newProductForm();
            newProduct.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            statisticsForm newStatForm = new statisticsForm();
            newStatForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            findClientOrdersForm newFind = new findClientOrdersForm();
            newFind.ShowDialog();
        }
    }
}
