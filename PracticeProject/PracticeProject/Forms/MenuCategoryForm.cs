using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeProject.Models;
using System.Windows.Forms;
using PracticeProject.DAL;

namespace PracticeProject.Forms
{
    public partial class MenuCategoryForm : Form
    {
        private readonly MenuDbContext _context;
        MenuCategory secilmisKategoriya;

        public MenuCategoryForm()
        {
            InitializeComponent();
            _context = new MenuDbContext();
            secilmisKategoriya = new MenuCategory();
        }

        private void MenuCategoryForm_Load(object sender, EventArgs e)
        {
            FillCategories();
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text))
            {
                MessageBox.Show("Kateqoriya adını daxil edin!");
                return;
            }

            if (txtCategoryName.Text.Length < 2)
            {
                MessageBox.Show("Kateqoriya adı minimum 2 simvol olmalidir");
                return;
            }

            MenuCategory category = new MenuCategory();
            category.Name = txtCategoryName.Text;

            _context.Categories.Add(category);
            _context.SaveChanges();

            MessageBox.Show($"{category.Name} əlavə olundu");
            FillCategories();
        }

        public void FillCategories()
        {
            dgvCategories.Rows.Clear();

            foreach (MenuCategory item in _context.Categories.ToList())
            {
                dgvCategories.Rows.Add(item.Id, item.Name);
            }

            Reset();
        }

        private void BtnUpdateCategory_Click(object sender, EventArgs e)
        {
            if (secilmisKategoriya != null)
            {
                secilmisKategoriya.Name = txtCategoryName.Text;
                _context.SaveChanges();
                FillCategories();
            }
           
        }

        private void BtnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (secilmisKategoriya != null)
            {
                _context.Categories.Remove(secilmisKategoriya);
                _context.SaveChanges();
                FillCategories();
            }
          
        }

        private void DgvCategories_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = (int)dgvCategories.Rows[e.RowIndex].Cells[0].Value;
            secilmisKategoriya = _context.Categories.FirstOrDefault(c => c.Id == id);
            txtCategoryName.Text = secilmisKategoriya.Name;
        }

        public void Reset()
        {
            secilmisKategoriya = null;
            txtCategoryName.Text = "";
        }
    }
}
