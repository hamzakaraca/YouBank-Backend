using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Credit:IEntity
    {
        public int Id { get; set; }
        public int QuantityOfCredit { get; set; }
        public int CustomerId { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime FinishDateOfCredit { get; set; }

    }
}
