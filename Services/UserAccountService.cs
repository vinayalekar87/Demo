using BankSystemApp.Interfaces;
using BankSystemApp.Models;

namespace BankSystemApp.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<List<UserAccountModel>> GetAllUserAccount()
        {
            return await _userAccountRepository.GetAllUserAccount();
        }

        public async Task<int> CreateUserAccount(UserAccountModel value)
        {
            return await _userAccountRepository.CreateUserAccount(value);
        }
        public async Task UpdateUserAccount(int id, UserAccountModel value)
        {

            await _userAccountRepository.UpdateUserAccount(id, value);
        }

        public async Task DeleteUserAccount(int id)
        {
            await _userAccountRepository.DeleteUserAccount(id);
        }

        public async Task Deposit(int id, int amount)
        {
            try
            {
                if (amount > 10000)
                {
                    throw new ApplicationException("A user cannot deposit more than $10,000 in a single transaction.");
                }
                await _userAccountRepository.Deposit(id, amount);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task Withdraw(int id, int amount)
        {
            try
            {
                UserAccountModel userBalance = await _userAccountRepository.GetUserAccountDetailsById(id);
                if (100 > (userBalance.Balance - amount))
                {
                    throw new ApplicationException("An account cannot have less than $100 at any time in an account.");
                }
                if (amount > (userBalance.Balance * 0.9))
                {
                    throw new ApplicationException("A user cannot withdraw more than 90% of their total balance from an account in a single transaction.");
                }
                await _userAccountRepository.Withdraw(id, amount);
            }
            catch (Exception)
            {
                throw;
            }
            
        }


    }
}
