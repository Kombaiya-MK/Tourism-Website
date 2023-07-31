using UserAPI.Models.DTO;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Services
{
    public class ManageUserService : IManageUser
    {
        private readonly ICommandRepo<TravelAgent, string> _cmdTravelRepo;
        private readonly ICommandRepo<UserDetails, string> _cmdDetailsRepo;
        private readonly ICommandRepo<User, string> _cmdUserRepo;
        private readonly IQueryRepo<TravelAgent, string> _qryTravelRepo;
        private readonly IQueryRepo<UserDetails, string> _qryDetailsRepo;
        private readonly IQueryRepo<User, string> _qryUserRepo;

        public ManageUserService(ICommandRepo<TravelAgent , string> commandTravelRepo , ICommandRepo<UserDetails, string> commandDetailsRepo, 
            ICommandRepo<User, string> commandUserRepo, IQueryRepo<TravelAgent,string> queryTravelRepo, 
            IQueryRepo<UserDetails, string> queryDetailsRepo, IQueryRepo<User, string> queryUserRepo)
        {
            _cmdTravelRepo = commandTravelRepo;
            _cmdDetailsRepo = commandDetailsRepo;
            _cmdUserRepo = commandUserRepo;
            _qryTravelRepo = queryTravelRepo;
            _qryDetailsRepo = queryDetailsRepo;
            _qryUserRepo = queryUserRepo;
        }

        //Get All Services of all models

        //Get All Users
        public async Task<ICollection<User>> GetAllUsers()
        {
            var users = await _qryUserRepo.GetAll();
            if (users.Count == 0)
                throw new NoDataException("No data available ");
            return users;
        }

        //Get All Userdetails
        public async Task<ICollection<UserDetails>> GetAllUserDetails()
        {
            var details = await _qryDetailsRepo.GetAll();
            if (details.Count == 0)
                throw new NoDataException("No data available ");
            return details;
        }

        //Get All TravelAgents
        public async Task<ICollection<TravelAgent>> GetAllTravelAgents()
        {
            var agents = await _qryTravelRepo.GetAll();
            if (agents.Count == 0)
                throw new NoDataException("No data available ");
            return agents;
        }

        public Task<UserDTO> Login(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> Register(RegisterDTO register)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(PasswordDTO passwordDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<User> ApproveAgent(ApproveAgentDTO agentDTO)
        {
            var user = new User
            {
                Email = agentDTO.Email,
                Status = agentDTO.Status
            };
            return await _cmdUserRepo.Update(user);
        }
    }
}
