using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoanWeb.Models
{
    public class Compare
    {
        List<Product> pros = new List<Product>();
        public IEnumerable<Product> Pros
        {
            get { return pros; }
        }
        public void Add(Product pro)
        {
            var product = pros.FirstOrDefault(s => s.idProduct == pro.idProduct);
            if (product == null)
            {
                pros.Add(pro);

            }
           
        }

        public void ClearCompare()
        {
            pros.Clear();
        }
        public void Delete(int id)
        {
            pros.RemoveAll(s => s.idProduct == id);
        }
    }
}