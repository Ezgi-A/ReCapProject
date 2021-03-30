using Core.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Payment:IEntity
    {
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public string CreditCardNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
    }
}
