using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Souq.Models.MyClasses
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderCost { get; set; }
        public bool Status { get; set; }

        [ForeignKey("payment")]
        public int payment_ID { get; set; }
        public virtual Payment payment { get; set; }

        //[ForeignKey("Customer")]
       // public int Customer_ID { get; set; }
        public virtual ApplicationUser Customer { get; set; }
    }
}