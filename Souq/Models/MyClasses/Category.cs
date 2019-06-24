using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Souq.Models.MyClasses
{
    public class Category
    {
        public int ID { get; set; }
        [StringLength(maximumLength: 30, ErrorMessage = "Maximum Name length is 30 letters")]
        [Required(ErrorMessage = "Please Enter Category Name")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please Insert Category Description")]
        public string CategoryDescription { get; set; }
        public bool CategoryStatus { get; set; }
        public virtual ICollection<Product> Products { set; get; } = new List<Product>();
    }
}