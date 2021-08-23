using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prepaid_Mobile_Topup_MVC.Models
{
    //Prepaid customer information
    public class PrepaidCustomer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistereddDate { get; set; }

    }
}
