using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTwebApi
{
    //this class will get me all the valid user and password
    public class OrderSecurity
    {
        //method to retrieve the values
        //boolean type 
        //will just teell me is a valid user or not

        public static bool login(string username, string password)
        {
            using (OrdersDBEntities entity = new OrdersDBEntities())
            {
                return entity.users.Any((u) => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
            }
        }
    }
}