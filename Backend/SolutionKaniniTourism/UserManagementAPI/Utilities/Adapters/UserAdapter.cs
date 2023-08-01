using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Models.DTO;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Utilities.Adapters
{
    public class UserAdapter : IAdapter
    {
        public User UserDTOtoUserAdapter(UserDTO register)
        {
            var user = new User();
            var hmac = new HMACSHA512();
            user.Email = register.Email;
            user.Role = register.Role;
            user.HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password ?? "1234"));
            user.Password = hmac.Key;
            user.Status = register.Status;
            return user;
        }
    }
}
