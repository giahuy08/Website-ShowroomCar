using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoanWeb.Models.ViewModels
{
    public class CompareProductViewModel
    {
        public Product Product1
        {
            get;set;
        }
        public Product Product2 { get; set; }

        public List<Product> pros { get; set; }
    }
}