using System;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account();
            account.CostumerId = 1;
            account.Money = "60000";
            account.AccountNumber = "4800017"; 

            AccountManager accountManager = new AccountManager(new EfAccountDal());
            accountManager.Add(account);

            
        }
    }
}
