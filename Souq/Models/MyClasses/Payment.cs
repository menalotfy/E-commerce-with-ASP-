using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Souq.Models.MyClasses
{
    public class Payment
    {
        public int ID { get; set; }
        public string paymentType { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Order> Orders { set; get; }
    }
}