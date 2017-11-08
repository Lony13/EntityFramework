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
    public partial class newProductForm : Form
    {
        ProdContent pDB;

        public newProductForm()
        {
            InitializeComponent();
            pDB = new ProdContent();

            using (var prodDB = new ProdContent())
            {
                var categories = from c in prodDB.Category
                                orderby c.Name
                                select c.Name;

                foreach (var c in categories)
                {
                    comboBox2.Items.Add(c);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var vProductName = textBox1.Text;
            var vUnitsInStock = Int32.Parse(textBox2.Text);
            var vCategory = (String)comboBox2.SelectedItem;
            var vUnitPrice = textBox3.Text;

            using (var prodDB = new ProdContent())
            {
                var catId = (from c in prodDB.Category
                            where c.Name == vCategory
                            select c.CategoryId).FirstOrDefault();

                if (catId == 0)
                {
                    MessageBox.Show("You have to choose category!");
                    return;
                }

                var isProductExists = from p in prodDB.Product
                                          where p.Name == vProductName
                                          select p.Name;

                if (isProductExists.Contains(vProductName))
                {
                    MessageBox.Show("Product with this name already exists");
                    return;
                }

                var newProduct = new Product();
                newProduct.Name = vProductName;
                newProduct.UnitsInStock = vUnitsInStock;
                newProduct.CategoryId = catId;
                newProduct.Unitprice = Decimal.Parse(vUnitPrice);
                prodDB.Product.Add(newProduct);
                prodDB.SaveChanges();
            }

            Hide();
            DestroyHandle();
        }

        private void newProductForm_Load(object sender, EventArgs e)
        {
            pDB.Product.Load();
            productBindingSource.DataSource = pDB.Product.Local.ToBindingList();
        }
    }
}
