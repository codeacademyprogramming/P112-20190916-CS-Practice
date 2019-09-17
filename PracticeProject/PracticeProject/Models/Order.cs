using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Models
{
    class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int Id { get; set; }
        public DateTime CreatedAt{ get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
