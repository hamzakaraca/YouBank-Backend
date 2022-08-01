using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.DTOs
{
    public class AccountDto:IDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime AccountCreateDate { get; set; } = DateTime.Now;
        public string Money { get; set; }
    }
}
