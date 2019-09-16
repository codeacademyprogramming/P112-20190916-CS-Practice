using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Models
{
    class MenuCategory
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}
