using PracticeProject.DAL;
using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PracticeProject.Forms
{
    public partial class MenuForm : Form
    {
        private readonly MenuDbContext _context;

        Models.MenuItem selectedMenuItem;

        public MenuForm()
        {
            InitializeComponent();
            _context = new MenuDbContext();
            selectedMenuItem = new Models.MenuItem();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            FillCombobox();
            FillMenuItems();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Validations
            if (string.IsNullOrEmpty(txtItemName.Text) || 
                string.IsNullOrEmpty(txtItemPrice.Text) ||
                cbxItemCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Məlumatları tam doldurun");
                return;
            }


            Models.MenuItem mItem = new Models.MenuItem();
            mItem.Name = txtItemName.Text;
            mItem.Price = Convert.ToDouble(txtItemPrice.Text);

            string catName = cbxItemCategory.SelectedItem.ToString();
            MenuCategory menuCategory = _context.Categories.FirstOrDefault(c => c.Name == catName);


            mItem.CategoryId = menuCategory.Id;

            _context.MenuItems.Add(mItem);
            _context.SaveChanges();

            FillMenuItems();
        }

        public void FillCombobox()
        {

            foreach (MenuCategory item in _context.Categories.ToList())
            {
                cbxItemCategory.Items.Add(item.Name);
            }
        }


        public void FillMenuItems()
        {
            dgvMenuItems.Rows.Clear();
            List<Models.MenuItem> menuItems = _context.MenuItems.ToList();

            foreach (var item in menuItems)
            {
                dgvMenuItems.Rows.Add(item.Id, item.Name, item.Price, item.Category.Name);
            }
            Reset();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedMenuItem == null)
            {
                return;
            }
            selectedMenuItem.Name = txtItemName.Text;
            selectedMenuItem.Price = Convert.ToDouble(txtItemPrice.Text);

            string categoryName = cbxItemCategory.SelectedItem.ToString();
            MenuCategory menuCategory = _context.Categories.FirstOrDefault(c => c.Name == categoryName);

            selectedMenuItem.CategoryId = menuCategory.Id;

            _context.SaveChanges();

            FillMenuItems();

        }

        private void dgvMenuItems_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedItemID = (int)dgvMenuItems.Rows[e.RowIndex].Cells[0].Value;
            selectedMenuItem = _context.MenuItems.FirstOrDefault(m => m.Id == selectedItemID);

            txtItemName.Text = selectedMenuItem.Name;
            txtItemPrice.Text = selectedMenuItem.Price.ToString();
            cbxItemCategory.SelectedItem = selectedMenuItem.Category.Name;
        }

        public void Reset()
        {
            txtItemName.Text = "";
            txtItemPrice.Text = "";
            cbxItemCategory.SelectedIndex = -1;

            selectedMenuItem = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMenuItem == null)
            {
                return;
            }

            _context.MenuItems.Remove(selectedMenuItem);
            _context.SaveChanges();

            FillMenuItems();

        }
    }
}
