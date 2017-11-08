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
    public partial class NewCustomerForm : Form
    {
        ProdContent pDB;

        public NewCustomerForm()
        {
            InitializeComponent();
            pDB = new ProdContent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var vCompanyName = textBox1.Text;
            var vDescription = textBox2.Text;

            using (var prodDB = new ProdContent())
            {
                var isCompanyNameExists = from c in prodDB.Customers
                                          where c.CompanyName == vCompanyName
                                          select c.CompanyName;

                if (isCompanyNameExists.Contains(vCompanyName))
                {
                    MessageBox.Show("Company with this name exists");
                    return;
                }

                var newCustomer = new Customer();
                newCustomer.CompanyName = vCompanyName;
                newCustomer.Description = vDescription;
                prodDB.Customers.Add(newCustomer);
                prodDB.SaveChanges();
            }

            Hide();
            DestroyHandle();
        }

        private void NewCustomerForm_Load(object sender, EventArgs e)
        {
            pDB.Customers.Load();
            customerBindingSource.DataSource = pDB.Customers.Local.ToBindingList();
        }
    }
}
