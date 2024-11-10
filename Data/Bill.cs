using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Data
{
    public class Bill
    {
        public Bill() { }

        public Bill(string billerName , string email , string location)
        {
            this.billerName = billerName;
            this.email = email; 
            this.location = location;

        }

       public string billerName { set; get; }
       public string email { set; get; }
       public string location { set; get; }
    }
}
