using Souq.Models.MyClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Souq.ViewModels
{
    public class ProductMetaDataVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Enter Product Price")]
        [RegularExpression(pattern: "^[1-9][0-9]*$", ErrorMessage = "Price must be at least 1 EGP")]

        public double ProductPrice { get; set; }
        [Required(ErrorMessage = "Please Enter Product Quantity")]
        [RegularExpression(pattern: "^[1-9][0-9]*$", ErrorMessage = "Quantity must be at least 1 product")]

        public int ProductQuantity { get; set; }

        [Required(ErrorMessage = "Please Enter Product Discount")]
        // [CheckQuantity]
        [Remote(action: "CompareProductPriceDiscount", controller: "Supplier", AdditionalFields = "ProductPrice", ErrorMessage = "Product Discount must be less than Product Price")]
        // [Range(minimum: 0, maximum: 9999, ErrorMessage = "Discount rang between 0: 9999")]

        public double ProductDiscount { get; set; }
        public bool Status { get; set; }
        public int Category_ID { get; set; }

     
        [StringLength(maximumLength: 20, ErrorMessage ="Name maximum length = 20")]
        [Required(ErrorMessage = "Please Enter Product Name")]
        [RegularExpression(pattern: @"\w{3,50}", ErrorMessage = "Name must be between 3 : 50 letters")]
        //[Remote("CheckProductName", "Supplier", ErrorMessage = "Product Name Already Exists please replace it")]
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
       


        public List<Category> categories { get; set; } = new List<Category>();

    }
}