using BankSystemApp.Interfaces;
using BankSystemApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace BankSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        #region GET
        /// <summary>
        /// Get Action return user information.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userAccountService.GetAllUserAccount();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }



        #endregion GET

        #region POST

        /// <summary>
        /// Create new user account
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserAccountModel value)
        {
            var result = await _userAccountService.CreateUserAccount(value).ConfigureAwait(false);
            return Ok(result);
        }

        #endregion

        #region PUT

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserAccountModel value)
        {
            
            await _userAccountService.UpdateUserAccount(id, value);
            return Ok();
        }

        [HttpPut("Deposit/{id}/{amount}")]
        public async Task<IActionResult> Deposit(int id, int amount)
        {            
            await _userAccountService.Deposit(id, amount);
            return Ok();
        }

        [HttpPut("Withdraw/{id}/{amount}")]
        public async Task<IActionResult> Withdraw(int id, int amount)
        {
            await _userAccountService.Withdraw(id, amount);
            return Ok();
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Delete user account from memory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _userAccountService.DeleteUserAccount(id);
            return Ok();
        }

        #endregion
    }
}
