using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Account:IEntity
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime AccountCreateDate { get; set; }
        public int Money { get; set; }
    }
}
