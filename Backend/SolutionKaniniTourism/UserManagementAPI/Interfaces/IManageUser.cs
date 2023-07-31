using UserAPI.Models.DTO;
using UserManagementAPI.Models;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Interfaces
{
    public interface IManageUser
    {
        Task<UserDTO> Login(UserDTO userDTO);
        Task<UserDTO> Register(RegisterDTO register);
        Task<bool> ChangePassword(PasswordDTO passwordDTO);
        Task<User> ApproveAgent(ApproveAgentDTO agentDTO);
        Task<ICollection<User>> GetAllUsers();
        Task<ICollection<UserDetails>> GetAllUserDetails();
        Task<ICollection<TravelAgent>> GetAllTravelAgents();

    }
}
