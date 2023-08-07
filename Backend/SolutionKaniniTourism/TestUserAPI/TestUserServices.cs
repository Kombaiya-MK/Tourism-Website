
using Moq;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Interfaces;
using UserAPI.Models.DTO;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace TestUserAPI
{
    [TestClass]
    public class TestUserService
    {
        private Mock<ICommandRepo<TravelAgent, string>>? _cmdTravelRepoMock;
        private Mock<ICommandRepo<UserDetails, string>>? _cmdDetailsRepoMock;
        private Mock<ICommandRepo<User, string>>? _cmdUserRepoMock;
        private Mock<ICommandRepo<VerificationCodes, string>>? _cmdCodeRepoMock;
        private Mock<IQueryRepo<TravelAgent, string>>? _qryTravelRepoMock;
        private Mock<IQueryRepo<UserDetails, string>>? _qryDetailsRepoMock;
        private Mock<IQueryRepo<User, string>>? _qryUserRepoMock;
        private Mock<IQueryRepo<VerificationCodes, string>>? _qryCodeRepoMock;
        private Mock<ITokenGenerate>? _tokenServiceMock;
        private Mock<IAdapter>? _adapterMock;

        private ManageUserService _userService;

        [TestInitialize]
        public void Initialize()
        {
            _cmdTravelRepoMock = new Mock<ICommandRepo<TravelAgent, string>>();
            _cmdDetailsRepoMock = new Mock<ICommandRepo<UserDetails, string>>();
            _cmdUserRepoMock = new Mock<ICommandRepo<User, string>>();
            _cmdCodeRepoMock = new Mock<ICommandRepo<VerificationCodes, string>>();
            _qryTravelRepoMock = new Mock<IQueryRepo<TravelAgent, string>>();
            _qryDetailsRepoMock = new Mock<IQueryRepo<UserDetails, string>>();
            _qryUserRepoMock = new Mock<IQueryRepo<User, string>>();
            _qryCodeRepoMock = new Mock<IQueryRepo<VerificationCodes, string>>();
            _tokenServiceMock = new Mock<ITokenGenerate>();
            _adapterMock = new Mock<IAdapter>();

            _userService = new ManageUserService(
                _cmdTravelRepoMock.Object,
                _cmdDetailsRepoMock.Object,
                _cmdUserRepoMock.Object,
                _qryTravelRepoMock.Object,
                _qryDetailsRepoMock.Object,
                _qryUserRepoMock.Object,
                _qryCodeRepoMock.Object,
                _cmdCodeRepoMock.Object,
                _tokenServiceMock.Object,
                _adapterMock.Object
            );

            _adapterMock.Setup(a => a.UserDTOtoUserAdapter(It.IsAny<UserDTO>())).Returns((UserDTO userDTO) =>
            {
                return new User
                {
                    Email = userDTO.Email,
                    HashKey = new byte[] { 0, 1, 2, 3 },
                    Password = new byte[] { 4, 5, 6, 7 }
                };
            });

            // Set up default behavior for user query repo mock
            _qryUserRepoMock.Setup(q => q.Get(It.IsAny<string>())).ReturnsAsync((string email) =>
            {
                return new User
                {
                    Email = email,
                    HashKey = new byte[] { 0, 1, 2, 3 },
                    Password = new byte[] { 4, 5, 6, 7 }
                };
            });

            // Set up default behavior for user command repo mock
            _cmdUserRepoMock.Setup(c => c.Add(It.IsAny<User>())).ReturnsAsync((User user) => user);
            _cmdUserRepoMock.Setup(c => c.Update(It.IsAny<User>())).ReturnsAsync((User user) => user);

            // Set up default behavior for token service mock
            _tokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<UserDTO>())).Returns("fake-token");
        }

        [TestMethod]
        public async Task GetAllUsers_NoUsers_ThrowsException()
        {
            _qryUserRepoMock?.Setup(q => q.GetAll()).ReturnsAsync(new List<User>());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NoDataException>(() => _userService.GetAllUsers());
        }

        [TestMethod]
        public async Task GetAllUserDetails_NoDetails_ThrowsException()
        {
            _qryDetailsRepoMock?.Setup(q => q.GetAll()).ReturnsAsync(new List<UserDetails>());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NoDataException>(() => _userService.GetAllUserDetails());
        }

        [TestMethod]
        public async Task GetAllTravelAgents_NoAgents_ThrowsException()
        {
            _qryTravelRepoMock?.Setup(q => q.GetAll()).ReturnsAsync(new List<TravelAgent>());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NoDataException>(() => _userService.GetAllTravelAgents());
        }

        [TestMethod]
        public async Task Register_ValidUser_ReturnsUserDTO()
        {
            var userDTO = new UserDTO
            {
                Role = "Admin",
                Email = "Admin@gmail.com",
                Password = "admin@123",
                Status = "Active"
            };
            var user = new User
            {
                Role = "Admin",
                Email = "Admin@gmail.com",
                HashKey = new byte[] { 0, 1, 2, 3 },
                Password = new byte[] { 4, 5, 6, 7 },
                Status = "Active"
            };

            _adapterMock?.Setup(a => a.UserDTOtoUserAdapter(userDTO)).Returns(user);
            _cmdUserRepoMock?.Setup(c => c.Add(user)).ReturnsAsync(user);
            _tokenServiceMock?.Setup(t => t.GenerateToken(It.IsAny<UserDTO>())).Returns("fake-token");

            var result = await _userService.Register(userDTO);

            Assert.IsNotNull(result);
            Assert.AreEqual(userDTO.Email, result.Email);
            Assert.AreEqual(userDTO.Role, result.Role);
            Assert.AreEqual("fake-token", result.Token);
        }

        [TestMethod]
        public async Task Login_ValidCredentials_ReturnsUserDTO()
        {
            // Arrange
            var userEmail = "user@example.com";
            var userPassword = "password";

            var hashedPassword = new HMACSHA512().ComputeHash(Encoding.UTF8.GetBytes(userPassword));

            var userMock = new Mock<IQueryRepo<User, string>>();
            userMock.Setup(q => q.Get(userEmail)).ReturnsAsync(new User
            {
                Email = userEmail,
                Password = hashedPassword,
                HashKey = hashedPassword
            });

            _tokenServiceMock?.Setup(t => t.GenerateToken(It.IsAny<UserDTO>())).Returns("fake-token");

            var userDTO = new UserDTO
            {
                Email = userEmail,
                Password = userPassword
            };

            // Act
            var result = await _userService.Login(userDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userDTO.Email, result.Email);
            Assert.AreEqual("fake-token", result.Token);
        }



        [TestMethod]
        public async Task ApproveAgent_ValidObject_ReturnsUser()
        {
            //Arrange
            ApproveAgentDTO agentDTO = new()
            {
                Email = "agent@gmail.com",
                Status = "Approved"
            };

            //Act
            var result = await _userService.ApproveAgent(agentDTO);

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task GetAllTravelAgents_Valid_ReturnsListOfAgents()
        {
            // Arrange
            var travelAgentList = new List<TravelAgent>
            {
                new TravelAgent { AgencyEmail = "agent1@example.com", AgencyName = "Agent 1" },
                new TravelAgent { AgencyEmail = "agent2@example.com", AgencyName = "Agent 2" }
            };

            _qryTravelRepoMock?.Setup(q => q.GetAll()).ReturnsAsync(travelAgentList);

            // Act
            var result = await _userService.GetAllTravelAgents();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(travelAgentList.Count, result.Count);
            foreach (var expectedAgent in travelAgentList)
            {
                var matchingResultAgent = result.FirstOrDefault(a => a.AgencyEmail == expectedAgent.AgencyEmail && a.AgencyName == expectedAgent.AgencyName);
                Assert.IsNotNull(matchingResultAgent);
            }
        }


        [TestMethod]
        public async Task GetAllUserDetails_Valid_ReturnsListOfUserDetails()
        {
            // Arrange
            var userdetails = new List<UserDetails>
            {
                new UserDetails () { Email = "user1@gmail.com" , UserName = "User1"},
                new UserDetails () { Email = "user2@gmail.com" , UserName = "User2"}
            };

            _qryDetailsRepoMock?.Setup(q => q.GetAll()).ReturnsAsync(userdetails);

            // Act
            var result = await _userService.GetAllUserDetails();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userdetails.Count, result.Count);
            foreach (var expectedAgent in userdetails)
            {
                var matchingResultAgent = result.FirstOrDefault(a => a.Email == expectedAgent.Email && a.UserName == expectedAgent.UserName);
                Assert.IsNotNull(matchingResultAgent);
            }
        }

        [TestMethod]
        public async Task AddTravelAgency_ValidInput_ReturnsAgencyDTO()
        {
            
            //Arrange
            AgencyDTO agencyDTO = new()
            {
                AgencyEmail = "agency1@gmail.com",
                AgencyName = "Agency1"
            };

            TravelAgent agent = new()
            {
                Id = 1,
                AgencyName = "Agency1",
                AgencyEmail = "agency1@gmail.com",
                AgencyAddress = "123,ABS st , CBE",
                AgencyPhone = "9994291196",
                Email = "agent1@gmail.com"
            };

            //Act
            _cmdTravelRepoMock?.Setup(c => c.Add(agent)).ReturnsAsync(agent);


            //Assert
            var result = await _userService.AddTravelAgency(agent);
            Assert.IsNotNull(result);
            Assert.AreEqual(agent.AgencyName, result.AgencyName);
            Assert.AreEqual(agent.AgencyEmail, result.AgencyEmail);
        }

        [TestMethod]
        public async Task UpdatePassword_ValidInput_ReturnsBool()
        {

            //Arrange
            UpdatePasswordDTO passwordDTO = new()
            {
                Password = "newPassword",
                Email = "Admin@gmail.com"
            };
            var user = new User
            {
                Role = "Admin",
                Email = "Admin@gmail.com",
                HashKey = new byte[] { 0, 1, 2, 3 },
                Password = new byte[] { 4, 5, 6, 7 },
                Status = "Active"
            };

            var userDTO = new UserDTO
            {
                Role = "Admin",
                Email = "Admin@gmail.com",
                Password = "newPassword",
                Status = "Active"
            };

            //Act
            _cmdUserRepoMock?.Setup(c => c.Update(user)).ReturnsAsync(user);
            _adapterMock?.Setup(a => a.UsertoDTOAdapter(It.IsAny<User>())).Returns(userDTO);


            //Assert
            var result = await _userService.UpdatePassword(passwordDTO);
            Assert.IsNotNull(result);
        }
    }
}