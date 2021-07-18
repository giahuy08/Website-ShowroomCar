using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoanWeb.Models
{
    public class Item
    {
        public Product product { get; set; }
        public int quantity { get; set; }
    }
    public class Order
    {
        List<Item> items = new List<Item>();
        public IEnumerable<Item> Items
        {
            get { return items; }
        }
      
        public void Add(Product pro, int _quantity =1)
        {
            var item = items.FirstOrDefault(s => s.product.idProduct == pro.idProduct);
            if(item==null)
            {
                items.Add(new Item
                {
                    product = pro,
                    quantity = _quantity
                }); ;
                    
            }
            else
            {
                item.quantity += _quantity;
            }
        }
        public void Update_quantity (int id,int _quantity)
        {
            var item =items.Find(s => s.product.idProduct == id);
            if(item!=null)
            {
                item.quantity = _quantity;
            }
        }
        public double Total ()
        {
            var total = items.Sum(s => s.product.priceProduct * s.quantity);
            return (double)total;
        }
        public void Delete(int id)
        {
            items.RemoveAll(s => s.product.idProduct == id);
        }
        public int Total_Quantity()
        {
            var total = items.Sum(s => s.quantity);
            return total;
        }
        public void ClearOrder()
        {
            items.Clear();
        }
    }
}