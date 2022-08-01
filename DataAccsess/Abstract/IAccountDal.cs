using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IAccountDal:IEntityRepository<Account>
    {
        public Account SearchAccount(int accountId);
        public Customer SearchCustomer(int customerId);
        public List<AccountDto> GetAccountsFullAttribute();
        public void DeleteById(int id);
    }
}
