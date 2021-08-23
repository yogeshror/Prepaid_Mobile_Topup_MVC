using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prepaid_Mobile_Topup_MVC.Models
{
    //Mobile Account details
    public class MobileAccount
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public decimal Balance { get; set; }

        public int PrepaidCustomerId { get; set; }

        public PrepaidCustomer PrepaidCustomer { get; set; }
    }
}
