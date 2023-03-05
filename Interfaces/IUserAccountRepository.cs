using BankSystemApp.Models;

namespace BankSystemApp.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<List<UserAccountModel>> GetAllUserAccount();
        Task<int> CreateUserAccount(UserAccountModel value);
        Task DeleteUserAccount(int id);
        Task UpdateUserAccount(int id, UserAccountModel value);
        Task Deposit(int id, int amount);
        Task Withdraw(int id, int amount);
        Task<UserAccountModel> GetUserAccountDetailsById(int id);
    }
}
