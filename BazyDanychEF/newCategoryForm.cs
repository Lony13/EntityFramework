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
    public partial class newCategoryForm : Form
    {
        ProdContent prodDB;

        public newCategoryForm()
        {
            InitializeComponent();
            prodDB = new ProdContent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var vCategoryName = textBox1.Text;
            var vDescription = textBox2.Text;

            using (var prodDB = new ProdContent())
            {
                var isCategoryExists = prodDB.Category
                    .Where(cat => cat.Name == vCategoryName)
                    .Select(c => c.Name);

                if (isCategoryExists.Contains(vCategoryName))
                {
                    MessageBox.Show("Category with this name already exists");
                    return;
                }

                var newCategory = new Category();
                newCategory.Name = vCategoryName;
                newCategory.Description = vDescription;
                prodDB.Category.Add(newCategory);
                prodDB.SaveChanges();
            }

            Hide();
            DestroyHandle();
        }

        private void newCategoryForm_Load(object sender, EventArgs e)
        {
            prodDB.Category.Load();
            categoryBindingSource.DataSource = prodDB.Category.Local.ToBindingList();
        }
    }
}
