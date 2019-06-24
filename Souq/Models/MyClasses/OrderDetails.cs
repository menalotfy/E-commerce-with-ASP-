using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Souq.Models.MyClasses
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int Quantity { set; get; }
        public bool Status { get; set; }
        [ForeignKey("Order")]
        public int Order_ID { get; set; }
        public virtual Order Order { get; set; }


        [ForeignKey("product")]
        public int Product_ID { get; set; }
        public Product product { get; set; }
    }
}