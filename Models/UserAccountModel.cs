using System.Numerics;

namespace BankSystemApp.Models
{
    public class UserAccountModel
    {
        public int Id { get; set; }
        public double ContactNumber { get; set; }
        public string UserName { get; set; }
        public int Balance { get; set; }
    }
}
