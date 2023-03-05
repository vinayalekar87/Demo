using System;
using BankSystemApp.Data;
using BankSystemApp.Interfaces;
using BankSystemApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BankSystemApp.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {

        public UserAccountRepository()
        {
            using (var context = new ApiContext())
            {
                var userAccounts = new List<UserAccountModel>
                {
                new UserAccountModel
                {
                    ContactNumber = 8983769626,
                    UserName = "Vinay",
                    Balance = 2000
                },
                new UserAccountModel
                {
                    ContactNumber = 9822456545,
                    UserName = "Raj",
                    Balance = 5000
                }
                };
                context.UserAccount.AddRange(userAccounts);
                context.SaveChanges();
            }
        }

        public async Task<List<UserAccountModel>> GetAllUserAccount() {
            using (var context = new ApiContext())
            {
                var list = context.UserAccount.ToList();
                return list;
            }
        }

        public async Task<UserAccountModel> GetUserAccountDetailsById(int id)
        {
            try
            {
                await using (var context = new ApiContext())
                {
                    var userAccount = (from u in context.Set<UserAccountModel>().AsNoTracking()
                                       where u.Id == id
                                       select new UserAccountModel
                                       {
                                           Id = u.Id,
                                           ContactNumber= u.ContactNumber,
                                           UserName = u.UserName,
                                           Balance = u.Balance,
                                       }).FirstOrDefault();

                    //return userAccount;

                    if (userAccount != null)
                    {
                        return userAccount;
                    }
                    else
                    {
                        throw new ApplicationException("User not found!!");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }           
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<int> CreateUserAccount(UserAccountModel value)
        {
            await using (var context = new ApiContext())
            {
                context.UserAccount.Add(value);
                context.SaveChanges();
            }

            //userAccountModels.Add(value);
            return 1;
        }

        public async Task UpdateUserAccount(int id, UserAccountModel value)
        {
            await using (var context = new ApiContext())
            {
                var userAccount = (from u in context.Set<UserAccountModel>().AsNoTracking()
                                   where u.Id == id
                                   select new UserAccountModel
                                   {
                                       Id = u.Id,
                                       ContactNumber = u.ContactNumber,
                                       UserName = u.UserName,
                                       Balance = u.Balance,
                                   }).FirstOrDefault();
                if (userAccount != null)
                {
                    userAccount.ContactNumber = value.ContactNumber; 
                    userAccount.UserName = value.UserName; 
                    userAccount.Balance= value.Balance;
                    context.UserAccount.Update(userAccount);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Delete user account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteUserAccount(int id)
        {
            await using (var context = new ApiContext())
            {
                var userAccount = (from u in context.Set<UserAccountModel>().AsNoTracking()
                                  where u.Id == id
                                  select new UserAccountModel
                                  {
                                      Id = u.Id,
                                      ContactNumber = u.ContactNumber,
                                      UserName = u.UserName,
                                      Balance = u.Balance,
                                  }).FirstOrDefault();
                if (userAccount != null)
                {
                    context.UserAccount.Remove(userAccount);
                    context.SaveChanges();
                }                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task Deposit(int id, int amount)
        {
            await using (var context = new ApiContext())
            {
                var userAccount = (from u in context.Set<UserAccountModel>().AsNoTracking()
                                   where u.Id == id
                                   select new UserAccountModel
                                   {
                                       Id = u.Id,
                                       ContactNumber = u.ContactNumber,
                                       UserName = u.UserName,
                                       Balance = u.Balance,
                                   }).FirstOrDefault();
                if (userAccount != null)
                {
                    userAccount.Id = id;
                    userAccount.Balance = userAccount.Balance + amount;

                    context.UserAccount.Update(userAccount);
                    context.SaveChanges();
                }
            }
        }

        public async Task Withdraw(int id, int amount)
        {
            try
            {
                await using (var context = new ApiContext())
                {
                    var userAccount = (from u in context.Set<UserAccountModel>().AsNoTracking()
                                       where u.Id == id
                                       select new UserAccountModel
                                       {
                                           Id = u.Id,
                                           ContactNumber = u.ContactNumber,
                                           UserName = u.UserName,
                                           Balance = u.Balance,
                                       }).FirstOrDefault();
                    if (userAccount != null)
                    {
                        if (amount >= userAccount.Balance)
                        {
                            throw new ApplicationException("Amount should be less than account balance");
                        }
                        userAccount.Balance = userAccount.Balance - amount;
                        context.UserAccount.Update(userAccount);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
