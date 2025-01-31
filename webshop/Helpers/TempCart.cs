using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Helpers
{
    public static class TempCart
    {
        private static List<object> _tempCart = new List<object>();

        public static void AddTempCart(object product)
        {
            _tempCart.Add(product);
        }

        public static List<object> GetTempCart()
        {
            return _tempCart; 
        }

        public static void RemoveFromTempCart(object product)
        {
            _tempCart.Remove(product);
        }

        public static void ClearTempCart()
        {
            _tempCart.Clear();
        }


    }
}
