using System.Security.Cryptography;
using System.Text;
using UserAPI.Interfaces;
using UserAPI.Models.DTO;
using UserAPI.Services;
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
        private readonly ICommandRepo<VerificationCodes, string> _cmdCodeRepo;
        private readonly IQueryRepo<TravelAgent, string> _qryTravelRepo;
        private readonly IQueryRepo<UserDetails, string> _qryDetailsRepo;
        private readonly IQueryRepo<User, string> _qryUserRepo;
        private readonly IQueryRepo<VerificationCodes, string> _qryCodeRepo;
        private readonly ITokenGenerate _tokenservice;
        private readonly IAdapter _adapter;
        private readonly Random random = new();

        public ManageUserService(ICommandRepo<TravelAgent , string> commandTravelRepo , ICommandRepo<UserDetails, string> commandDetailsRepo, 
            ICommandRepo<User, string> commandUserRepo, IQueryRepo<TravelAgent,string> queryTravelRepo, 
            IQueryRepo<UserDetails, string> queryDetailsRepo, IQueryRepo<User, string> queryUserRepo,
            IQueryRepo<VerificationCodes, string> queryCodeRepo, ICommandRepo<VerificationCodes, string> cmdCodeRepo,
            ITokenGenerate tokenService , IAdapter adapter)
        {
            _cmdTravelRepo = commandTravelRepo;
            _cmdDetailsRepo = commandDetailsRepo;
            _cmdUserRepo = commandUserRepo;
            _cmdCodeRepo = cmdCodeRepo;
            _qryTravelRepo = queryTravelRepo;
            _qryDetailsRepo = queryDetailsRepo;
            _qryUserRepo = queryUserRepo;
            _qryCodeRepo = queryCodeRepo;
            _tokenservice = tokenService;
            _adapter = adapter;
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

        //Login
        public async Task<UserDTO> Login(UserDTO user)
        {
            if (user.Email == null)
            {
                throw new NullValueException("User Email is Required!!!");
            }
            var userData = await _qryUserRepo.Get(user.Email);
            if (userData != null && userData.Password != null && user.Password != null)
            {
                var hmac = new HMACSHA512(userData.Password);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userData == null || userData.HashKey == null || userData?.HashKey[i] == null)
                        throw new NullValueException("HashKey value is null");
                    else if (userPass[i] != userData.HashKey[i])
                        throw new InvalidUserException("Invalid user");
                }
                user = new UserDTO
                {
                    Email = userData.Email,
                    Role = userData.Role
                };
                user.Token = _tokenservice.GenerateToken(user);
            }
            return user;
        }

        //Register
        public async Task<UserDTO> Register(UserDTO register)
        {
            var user =  _adapter.UserDTOtoUserAdapter(register);
            var code = new VerificationCodes
            {
                Email = user.Email,
                Codes = random.Next(100000, 999999)
            };
            var newuser = await _cmdUserRepo.Add(user);
            _ = _cmdCodeRepo.Add(code) ?? throw new UnableToAddException("Unable Add Verification Codes");
            var userDTO = new UserDTO();
            if (newuser != null)
            {
                userDTO.Email = newuser.Email;
                userDTO.Role =  newuser.Role;
                userDTO.Token = _tokenservice.GenerateToken(userDTO);
            }
            return userDTO;
        }

        //Change Password
        public async Task<bool> ChangePassword(PasswordDTO passwordDTO)
        {
            bool status = false;
            if (passwordDTO.Email == null || passwordDTO.CurrentPassword == null)
            {
                throw new NullValueException("User Email is Required!!!");
            }
            User user = await _qryUserRepo.Get(passwordDTO.Email);
            if (user == null)
                throw new EmptyValueException("No such user available in the database");
            else if (user.Password == null)
                throw new NullValueException("user password");
            if (ValidatePassword(passwordDTO.CurrentPassword, user))
            {
                var hmac = new HMACSHA512();
                user.HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordDTO.UpdatedPassword ?? "1234"));
                user.Password = hmac.Key;
            }
            var result = await _cmdUserRepo.Update(user);
            if (result != null)
            {
                return true;
            }
            return status;
        }

        private static bool ValidatePassword(string currentPassword, User user)
        {
            bool status = true;
            if (user.Password == null)
                throw new NullValueException("user password");
            var hmac = new HMACSHA512(user.Password);
            var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(currentPassword));
            for (int i = 0; i < userPass.Length; i++)
            {
                if (user == null || user.HashKey == null || userPass[i] != user.HashKey[i])
                    status = false;
            }
            return status;
        }

        //Forgot Password


        //Approve Agent
        public async Task<User> ApproveAgent(ApproveAgentDTO agentDTO)
        {
            var user = new User
            {
                Email = agentDTO.Email,
                Status = agentDTO.Status
            };
            return await _cmdUserRepo.Update(user);
        }

        //Add Travel Agency details
        public async Task<AgencyDTO> AddTravelAgency(TravelAgent agent)
        {
            _ = await _cmdTravelRepo.Add(agent)
                ?? throw new UnableToAddException("Unable to add travel agency details!!!");
            var agency = new AgencyDTO
            {
                AgencyEmail = agent.AgencyEmail,
                AgencyName = agent.AgencyName,
            };
            return agency;
        }

        //Add User details 
        public async Task<UserDetails> AddUserDetails(UserDetails userDetails)
        {
            _ = await _cmdDetailsRepo.Add(userDetails)
                ?? throw new UnableToAddException("Unable to add user details!!!");
            return userDetails;
        }


        public Task<bool> TriggerVerificationCodeToEmail(ForgotPasswordDTO item)
        {
            throw new NotImplementedException();
        }

        //Validate Verification Codes
        public async Task<bool> ValidateCode(ForgotPasswordDTO item)
        {
            bool result = false;
            if(item == null || item.Email == null)
                throw new NullValueException("Null object in Forgot Passowrd");
            var code = await _qryCodeRepo.Get(item.Email) ?? throw new NullValueException("Invalid Email ID");
            if (code.Codes == item.Code)
            {
                var newcode = GenerateCode(item.Code);
                var UpdatedCode = new VerificationCodes()
                {
                    Email = item.Email,
                    Codes = newcode
                };
                _ = await _cmdCodeRepo.Update(UpdatedCode);
                result = true;
            }   
            return result;
        }

        //Generate Updated Code
        public int GenerateCode(int prevcode)
        {
            int newcode = random.Next(100000, 999999);
            while(newcode == prevcode)
            {
                newcode = random.Next(100000, 999999);
            }
            return newcode;
        }

        //Update Password
        public async Task<UserDTO> UpdatePassword(UpdatePasswordDTO password)
        {
            var hmac = new HMACSHA512();
            var newpassword = password.Password ?? "Password@123";
            var user = new User
            {
                Email = password.Email,
                HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes(newpassword)),
                Password = hmac.Key,
            };
            _ = await _cmdUserRepo.Update(user) ?? throw new UnableToUpdateException("Unable update password!!!");
            var result = _adapter.UsertoDTOAdapter(user);
            return result;
        }
    }
}
