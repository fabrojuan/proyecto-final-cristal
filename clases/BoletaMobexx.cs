using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPSA_V2022.clases
{
    public class BoletaMobexx
    {
        public  decimal total { get; set; }
        public  string description { get; set; }
        public  int reference { get; set; }

        public  string currency { get; set; }

        public Boolean test { get; set; }

        public string return_url { get; set; }
        public string webhook { get; set; }

        public Customer customer { get; set; }


        public BoletaMobexx(string email, string name, string identification)
        {
            customer = new Customer();
            this.customer.identification = identification;
            this.customer.email = email;
            this.customer.name = name;
        }



    }
}          