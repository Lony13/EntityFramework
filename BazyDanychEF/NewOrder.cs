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
    public partial class NewOrder : Form
    {
        public NewOrder()
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

                var categories = from c in prodDB.Category
                                orderby c.Name
                                select c.Name;

                foreach (var c in categories)
                {
                    comboBox3.Items.Add(c);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedCustomer = (string)comboBox1.SelectedItem;
            var selectedProduct = (string)comboBox2.SelectedItem;
            int selectedQuantity = Int32.Parse(textBox1.Text);

            using (var prodDB = new ProdContent())
            {
                var availableQuantity = from p in prodDB.Product
                                        where p.Name == selectedProduct
                                        select p.UnitsInStock;

                if (selectedQuantity > availableQuantity.First())
                {
                    MessageBox.Show("Do not have enough product available");
                    return;
                }

                var selectedProductID = from p in prodDB.Product
                                        where p.Name == selectedProduct
                                        select p.ProductID;

                var vOrder = new Order();
                vOrder.CompanyName = selectedCustomer;
                vOrder.ProductID = selectedProductID.First();
                vOrder.Quantity = selectedQuantity;

                prodDB.Orders.Add(vOrder);
                prodDB.SaveChanges();
            }

            Hide();
            DestroyHandle();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var prodDB = new ProdContent())
            {
                var selectedCategory = (string)comboBox3.SelectedItem;

                var selectedCategoryID = (from c in prodDB.Category
                                         where c.Name == selectedCategory
                                         select c.CategoryId).First();

                var products = from p in prodDB.Product
                               where p.CategoryId == selectedCategoryID
                               select p.Name;

                comboBox2.Items.Clear();
                foreach (var p in products)
                {
                    comboBox2.Items.Add(p);
                }
            }

        }
    }
}
