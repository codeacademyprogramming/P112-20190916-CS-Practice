using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PracticeProject.DAL;

namespace PracticeProject.Forms
{
    public partial class OrderForm : Form
    {
        private readonly MenuDbContext _context;

        List<OrderItem> orderItems;

        public OrderForm()
        {
            InitializeComponent();
            orderItems = new List<OrderItem>();
            _context = new MenuDbContext();

            Demo();
        }
        
        private void OrderForm_Load(object sender, EventArgs e)
        {
            FillMenuItemsCombobox();
        }

        public void FillMenuItemsCombobox()
        {
            foreach (var item in _context.MenuItems.ToList())
            {
                cbxMenuItems.Items.Add(item.Name + " - " + item.Price);
            } 
        }

        private void btnAddOrderItem_Click(object sender, EventArgs e)
        {
            OrderItem item = new OrderItem();

            string selectedItem = cbxMenuItems.SelectedItem.ToString();
            string[] selectedItemElements = selectedItem.Split('-');
            selectedItem = selectedItemElements[0].Trim();

            Models.MenuItem menuItem = _context.MenuItems.FirstOrDefault(m=>m.Name == selectedItem);

            item.MenuItemId = menuItem.Id;
            item.Quantity = (int)numericQuantity.Value;
            orderItems.Add(item);
        }

        private void btnApproveOrder_Click(object sender, EventArgs e)
        {
            Order order = new Order()
            {
                OrderItems = orderItems,
                CreatedAt = DateTime.Now
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        




        public void Demo()
        {
            double sum = 0;
            Order order = _context.Orders.Include("OrderItems").FirstOrDefault(o => o.Id == 5);
            foreach (var item in order.OrderItems)
            {
                Models.MenuItem menuItem = _context.MenuItems.FirstOrDefault(m => m.Id == item.MenuItemId);
                sum += menuItem.Price * item.Quantity;
            }

            MessageBox.Show(sum.ToString());
        }
    }
}
