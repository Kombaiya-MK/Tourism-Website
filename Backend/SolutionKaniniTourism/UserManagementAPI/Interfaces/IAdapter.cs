using UserAPI.Models.DTO;
using UserManagementAPI.Models;

namespace UserManagementAPI.Interfaces
{
    public interface IAdapter
    {
        User UserDTOtoUserAdapter(UserDTO user);

    }
}
