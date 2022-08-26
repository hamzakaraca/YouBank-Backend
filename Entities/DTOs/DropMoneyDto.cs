using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class DropMoneyDto:IDto
    {
        public int Id { get; set; }
        public int Money { get; set; }

    }
}
