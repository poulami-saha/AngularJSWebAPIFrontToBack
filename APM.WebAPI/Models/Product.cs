using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APM.WebAPI.Models
{
    public class Product
    {
        public string Description  { get; set; }
        [Required(ErrorMessage ="Product Code is Required",AllowEmptyStrings =false)]
        [MinLength(6, ErrorMessage ="Product code mon length is 6")]
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "Product Name is Required", AllowEmptyStrings = false)]
        [MinLength(5, ErrorMessage = "Product code min length is 5")]
        [MaxLength(12, ErrorMessage = "Product code max length is 12")]
        public string ProductName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ProductId { get; set; }
    }
}