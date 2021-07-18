//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoanWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.DetailOrders = new HashSet<DetailOrder>();
        }
    
        public int idProduct { get; set; }
        public int idCategoryProduct { get; set; }
        public string nameProduct { get; set; }
        public decimal priceProduct { get; set; }
        public string modelProduct { get; set; }
        public string timeProduction { get; set; }
        public string originProduct { get; set; }
        public string descriptionProduct { get; set; }
        public string urlImageProduct { get; set; }
        public string color { get; set; }
        public int seat { get; set; }
        public string fuel { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
        public IEnumerable<Product> GetListProduct { get; set; }
    }
}
