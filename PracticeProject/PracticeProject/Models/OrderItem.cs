﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Models
{
    class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        public int Quantity { get; set; }
    }
}
