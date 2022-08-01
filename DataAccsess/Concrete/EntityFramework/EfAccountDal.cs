using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using System.Linq;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAccountDal:EfEntityRepositoryBase<Account,YouBankContext>,IAccountDal
    {
        public Account SearchAccount(int accountId)
        {
            using (YouBankContext context=new YouBankContext())
            {
                var result = context.Accounts.SingleOrDefault(a => a.Id == accountId);
                return result;
            }
        }

        public Customer SearchCustomer(int customerId)
        {
            using (YouBankContext context =new YouBankContext())
            {
                var result = context.Costumers.SingleOrDefault(c=>c.Id==customerId);
                return result;
            }
            
        }

        public List<AccountDto> GetAccountsFullAttribute()
        {
            using (YouBankContext context= new YouBankContext())
            {
                var result = from ac in context.Accounts
                    join cu in context.Costumers on ac.CustomerId equals cu.Id
                    select new AccountDto
                    {
                        Id = ac.Id, CustomerFullName = cu.CustomerFullName, AccountNumber = ac.AccountNumber,
                        AccountCreateDate = ac.AccountCreateDate, Money = ac.Money
                    };
                return result.ToList();
            }
        }

        public void DeleteById(int id)
        {
            using (YouBankContext context=new YouBankContext())
            {
                var result =context.Accounts.SingleOrDefault(c=>c.Id==id) ;
                var deletedAccount = context.Entry(result);
                deletedAccount.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
