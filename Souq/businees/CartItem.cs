using Souq.Models.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Souq.businees
{
    public class CartItem
    {
        public Product Product
        {
            get;
            set;
        }

    public int Quantity
    {
        get;
        set;
    }
}
}