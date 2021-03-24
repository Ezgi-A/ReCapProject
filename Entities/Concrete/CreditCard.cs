using Core.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCard:IEntity
    {
        public int CreditCardId { get; set; }
        public int CustomerID { get; set; }
        public string CreditCardNumber { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
    }
}
