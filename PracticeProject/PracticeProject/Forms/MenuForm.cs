using PracticeProject.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PracticeProject.Models;

namespace PracticeProject.Forms
{
    public partial class MenuForm : Form
    {
        private readonly MenuDbContext _context;
        public MenuForm()
        {
            InitializeComponent();
            _context = new MenuDbContext();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Validations



            Models.MenuItem mItem = new Models.MenuItem();
            mItem.Name = txtItemName.Text;
            mItem.Price = Convert.ToDouble(txtItemPrice.Text);

            string catName = cbxItemCategory.SelectedItem.ToString();
            MenuCategory menuCategory = _context.Categories.FirstOrDefault(c => c.Name == catName);


            mItem.CategoryId = menuCategory.Id;

            _context.MenuItems.Add(mItem);
            _context.SaveChanges();
        }

        public void FillCombobox()
        {
            
            foreach (MenuCategory item in _context.Categories.ToList())
            {
                cbxItemCategory.Items.Add(item.Name);

            }
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            FillCombobox();
        }
    }
}
