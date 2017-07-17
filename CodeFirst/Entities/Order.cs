using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Customer Customer { get; set; }
    }
}
