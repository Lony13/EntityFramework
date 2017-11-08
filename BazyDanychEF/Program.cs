using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            StartForm form = new StartForm();
            form.ShowDialog();
        }
    }

    public class ProdContent : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int UnitsInStock { get; set; }
        public int CategoryId { get; set; }
        [Column(TypeName = "Money")]
        public decimal Unitprice { get; set; }
        public virtual List<Order> orders { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; }
    }

    public class Customer
    {
        [Key]
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public virtual List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CompanyName { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
