using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Customer:IEntity
    {
        public int Id { get; set; }
        public string CustomerFullName { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalityNumber { get; set; }

    }
}
